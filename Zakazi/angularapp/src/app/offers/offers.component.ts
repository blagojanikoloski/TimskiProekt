import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { isSameDay } from 'date-fns';
import { Subject } from 'rxjs';
import { CalendarEvent, CalendarView, CalendarEventTimesChangedEvent, CalendarWeekViewBeforeRenderEvent } from 'angular-calendar';

interface Offer {
  postId: number;
  businessName: string;
  nameOfService: string;
  price: number;
  availabilityFrom: string;
  availabilityTo: string;
  name: string;
  surname: string;
  email: string;
  phoneNumber: string;
  businessId: number;
  imageUrl: string;
}

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css'],
  styles: [
    `
      .bg-disabled {
        background-color: #cccccc !important;
        cursor: not-allowed;
      }
    `
  ]
})
export class OffersComponent implements OnInit {

  allBusinesses: Offer[] = [];
  businessServices: any[] = [];
  isPopupOpen: boolean = false;
  selectedPostIds: number[] = [];
  businessId!: number;

  view: CalendarView = CalendarView.Week; 
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  refresh: Subject<any> = new Subject();


  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const serializedResult = params['searchResult'];
      if (serializedResult) {
        this.allBusinesses = JSON.parse(serializedResult) as Offer[];
        
      }
    });
  }

  getServices(businessId: number) {
    this.businessId = businessId;
    this.http.get<any>(`https://localhost:7200/api/Posts/ByBusiness/${businessId}`)
      .subscribe(
        response => {
          this.isPopupOpen = true;
          this.businessServices = response;
        },
        error => {
          console.error('Error booking appointment:', error);
        }
      );
  }

  openPopup() {
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedPostIds = [];

    let startDateHTML = document.getElementById('start-date-text') as HTMLElement;
    let endDateHTML = document.getElementById('end-date-text') as HTMLElement;
    startDateHTML?.style.setProperty('font-size', '30px');
    endDateHTML?.style.setProperty('display', 'none');
    this.startDate = '';
    this.endDate = '';
  }

  toggleSelection(postId: number) {
    const index = this.selectedPostIds.indexOf(postId);
    if (index === -1) {
      this.selectedPostIds.push(postId);
    } else {
      this.selectedPostIds.splice(index, 1);
    }
  }

  bookAppointment() {
    const userString = localStorage.getItem('user');
    if (!userString) {
      console.error('User information not found.');
      return;
    }

    const user = JSON.parse(userString);
    const token = this.jwtHelper.decodeToken(user);
    const userId: number = +token.nameid;

    const startTimestamp = localStorage.getItem('startTimestamp') ?? '';
    const endTimestamp = localStorage.getItem('endTimestamp') ?? '';

    const appointmentData = {
      timestamp: new Date().toISOString(),
      requestStatus: 0,
      postIds: this.selectedPostIds,
      businessId: this.businessId,
      clientId: userId,
      from: new Date(startTimestamp).toISOString(),
      to: new Date(endTimestamp).toISOString(),
    };

    this.http.post<any>('https://localhost:7200/api/Requests/request', appointmentData)
      .subscribe(
        response => {
          this.router.navigate(['/home']);
        },
        error => {
          console.error('Error booking appointment:', error);
        }
      );
  }




  startDate!: string;
  endDate!: string;

  hourSegmentClicked(event: { date: Date }): void {

    let startDateHTML = document.getElementById('start-date-text') as HTMLElement;
    let endDateHTML = document.getElementById('end-date-text') as HTMLElement;

    if (!this.startDate) {
      this.startDate = new Date(event.date).toLocaleString();
      startDateHTML?.style.setProperty('font-size', '20px');
      endDateHTML?.style.setProperty('display', 'unset');

    }
    else {
      this.endDate = new Date(event.date).toLocaleString();
      endDateHTML?.style.setProperty('font-size', '20px');

      const startTimestamp = new Date(this.startDate);
      const endTimestamp = new Date(this.endDate);

      // Store timestamps in localStorage
      localStorage.setItem('startTimestamp', startTimestamp.toISOString());
      localStorage.setItem('endTimestamp', endTimestamp.toISOString());
    }

  }


  // Define a minimum date (to disable everything in the past you can just write this)
  minDate: Date = new Date();


  beforeWeekViewRender(renderEvent: CalendarWeekViewBeforeRenderEvent) {
    renderEvent.hourColumns.forEach(hourColumn => {
      hourColumn.hours.forEach(hour => {
        hour.segments.forEach(segment => {
          if (
            segment.date.getHours() >= 6 &&
            segment.date.getHours() <= 5
          ) {
            segment.cssClass = 'bg-disabled';
          }
        });
      });
    });
  }

}

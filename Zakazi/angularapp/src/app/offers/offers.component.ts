import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { isSameDay } from 'date-fns';
import { Subject } from 'rxjs';
import { CalendarMonthViewDay, CalendarEvent, CalendarView, CalendarEventTimesChangedEvent } from 'angular-calendar';

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
}

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {
  @ViewChild('modalContent', { static: true }) modalContent!: TemplateRef<any>;
  @ViewChild('timeSelectionModal', { static: true }) timeSelectionModal!: TemplateRef<any>;

  allBusinesses: Offer[] = [];
  businessServices: any[] = [];
  isPopupOpen: boolean = false;
  selectedPostIds: number[] = [];
  businessId!: number;

  view: CalendarView = CalendarView.Week; // Default to Week view
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  refresh: Subject<any> = new Subject();
  selectedEventDate!: Date;
  selectedTime: { hour: number, minute: number } = { hour: 0, minute: 0 };
  activeDayIsOpen: boolean = true; // Declare activeDayIsOpen here

  private modalRef!: NgbModalRef;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private modal: NgbModal
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const serializedResult = params['searchResult'];
      if (serializedResult) {
        this.allBusinesses = JSON.parse(serializedResult) as Offer[];
        this.initializeCalendarEvents();
      }
    });
  }

  initializeCalendarEvents(): void {
    this.events = this.allBusinesses.map((offer: Offer) => ({
      start: new Date(offer.availabilityFrom),
      end: new Date(offer.availabilityTo),
      title: offer.nameOfService,
      color: { primary: '#1e90ff', secondary: '#D1E8FF' }
    }));
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

  dayClicked({ day }: { day: CalendarMonthViewDay }): void {
    const date: Date = day.date;
    const events: CalendarEvent[] = day.events;
    if ((isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) || events.length === 0) {
      this.activeDayIsOpen = false;
    } else {
      this.activeDayIsOpen = true;
    }
    this.viewDate = date;
  }

  openModal(): void {
    this.modal.open(this.modalContent);
  }

  closeModal(): void {
    this.modal.dismissAll();
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

    // Close any open modal (if any)
    if (this.modalRef) {
      this.modalRef.close();
    }
  }

}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
// Define the interface for Offer
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
  allBusinesses: any;
  businessServices: any[] = [];
  isPopupOpen: boolean = false;
  selectedPostIds: number[] = [];

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
  }

  ngOnInit(): void {
    // Retrieve serialized search result from route parameters
    this.route.queryParams.subscribe(params => {
      const serializedResult = params['searchResult'];
      if (serializedResult) {
        // Deserialize the serialized result back to objects
        this.allBusinesses = JSON.parse(serializedResult) as Offer[];
        console.log(this.allBusinesses);
      }
    });
  }

  getServices(businessId: number) {
    console.log(businessId);


    this.http.get<any>(`https://localhost:7200/api/Posts/ByBusiness/${businessId}`)
      .subscribe(
        response => {
          this.isPopupOpen = true;
          this.businessServices = response;
          console.log('Servies:', this.businessServices);
          // Handle success - e.g., show a success message to the user
        },
        error => {
          console.error('Error booking appointment:', error);
          // Handle error - e.g., show an error message to the user
        }
      );
  }

  openPopup() {
    this.isPopupOpen = true;
  }

  closePopup() {
    console.log('Closing popup');
    this.isPopupOpen = false;
    this.selectedPostIds = [];
  }


  toggleSelection(postId: number) {
    const index = this.selectedPostIds.indexOf(postId);
    if (index === -1) {
      // If postId is not in the array, add it
      this.selectedPostIds.push(postId);
    } else {
      // If postId is already in the array, remove it
      this.selectedPostIds.splice(index, 1);
    }
  }

  bookAppointment() {
    // Fetch user information from localStorage
    const userString = localStorage.getItem('user');
    if (!userString) {
      console.error('User information not found.');
      return;
    }

    const user = JSON.parse(userString);
    const token = this.jwtHelper.decodeToken(user);
    const userId: number = +token.nameid; // Parse nameid to number

    // Fetch start and end timestamps from localStorage
    const startTimestamp = localStorage.getItem('startTimestamp') ?? '';
    const endTimestamp = localStorage.getItem('endTimestamp') ?? '';

    // Create appointment data object
    const appointmentData = {
      timestamp: new Date().toISOString(),
      requestStatus: 0, // Assuming RequestStatus is an enum
      postIds: this.selectedPostIds,
      businessId: this.allBusinesses[0].businessId, // Assuming allBusinesses is populated with data
      clientId: userId,
      from: new Date(startTimestamp).toISOString(),
      to: new Date(endTimestamp).toISOString(),
      // You may need to include other data here from your component
      // For example, business name, service name, price, etc.
    };

    console.log(appointmentData);

    // Make HTTP POST request to the backend API
    this.http.post<any>('https://localhost:7200/api/Requests/request', appointmentData)
      .subscribe(
        response => {
          console.log('Appointment booked successfully:', response);
          this.router.navigate(['/home']);
          // Handle success - e.g., show a success message to the user
        },
        error => {
          console.error('Error booking appointment:', error);
          // Handle error - e.g., show an error message to the user
        }
      );
  }

    

 
}

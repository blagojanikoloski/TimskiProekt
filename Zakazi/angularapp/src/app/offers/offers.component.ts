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
  selectedPostIds: string[] = [];

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


  toggleSelection(postId: string) {
    const index = this.selectedPostIds.indexOf(postId);
    if (index === -1) {
      // If postId is not in the array, add it
      this.selectedPostIds.push(postId);
    } else {
      // If postId is already in the array, remove it
      this.selectedPostIds.splice(index, 1);
    }
  }

  bookAppointment(selectedPostIds: string[]) {
    console.log(selectedPostIds);
    // Here, you can use the selectedPostIds array to book the appointment
    // Example: Send selectedPostIds to your backend for processing
  }

  //bookAppointment(offer: Offer) {

  //  const userString = localStorage.getItem('user');
  //  if (userString) {
  //    const user = JSON.parse(userString);
  //    const token = this.jwtHelper.decodeToken(user);
  //    const userId: number = +token.nameid; // Parse nameid to number


  //    const startTimestamp = localStorage.getItem('startTimestamp') ?? '';
  //    const endTimestamp = localStorage.getItem('endTimestamp') ?? '';

  //    const appointmentData = {
  //      timestamp: new Date().toISOString(), 
  //      requestStatus: 0, // Assuming RequestStatus is an enum
  //      postId: offer.postId,
  //      businessId: offer.businessId,
  //      clientId: userId,
  //      from: new Date(startTimestamp).toISOString(),
  //      to: new Date(endTimestamp).toISOString()
  //    };

  //    console.log(appointmentData);

  //    // Make HTTP POST request to the backend API
  //    this.http.post<any>('https://localhost:7200/api/Requests/request', appointmentData)
  //      .subscribe(
  //        response => {
  //          console.log('Appointment booked successfully:', response);
  //          this.router.navigate(['/home']);
  //          // Handle success - e.g., show a success message to the user
  //        },
  //        error => {
  //          console.error('Error booking appointment:', error);
  //          // Handle error - e.g., show an error message to the user
  //        }
  //      );
  //  } else {
  //    console.error('User ID not found. Cannot book appointment.');
  //    // Handle scenario where user ID is not available
  //  }
  // }

    

 
}

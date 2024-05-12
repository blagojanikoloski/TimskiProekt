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
  searchResult: Offer[] = []; // Initialize searchResult as an empty array of Offer objects

  jwtHelper: JwtHelperService; // Declare jwtHelper property
  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    // Retrieve serialized search result from route parameters
    this.route.queryParams.subscribe(params => {
      const serializedResult = params['searchResult'];
      if (serializedResult) {
        // Deserialize the serialized result back to objects
        this.searchResult = JSON.parse(serializedResult) as Offer[];
        console.log(this.searchResult);
      }
    });
  }

  bookAppointment(offer: Offer) {

    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid; // Parse nameid to number

      // Inside your component class
      const appointmentData = {
        timestamp: new Date().toISOString(), // Assuming timestamp is a string representation of DateTime
        requestStatus: 0, // Assuming RequestStatus is an enum
        postId: offer.postId,
        businessId: offer.businessId,
        clientId: userId
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
    } else {
      console.error('User ID not found. Cannot book appointment.');
      // Handle scenario where user ID is not available
    }
    

    

  }
}

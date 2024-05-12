import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  requestOptions: any[] = []; // Initialize requestOptions as an empty array
  jwtHelper: JwtHelperService;
  constructor(private http: HttpClient, private router: Router) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.fetchAllRequests();
  }

  onEditOfferClick() {
    this.router.navigate(["offers/edit"]);
  }

  onDeleteOfferClick() {
    // Implement delete offer functionality if needed
  }

  onCancelOfferClick() {
    this.http.post<any>(`https://localhost:7200/api/Request/cancel`, {}).subscribe(
      (response) => {
        console.log('Request cancelled successfully:', response);
        this.router.navigate(['/home']); // Redirect to home upon successful cancellation
      },
      (error) => {
        console.error('Error cancelling request:', error);
      }
    );
  }

  isAdmin: boolean = false;

  fetchAllRequests(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;
      console.log(userId);

      this.http.get<any[]>(`https://localhost:7200/api/Requests/client/${userId}/requests`).subscribe(
        (requests: any[]) => {
          this.requestOptions = requests; // Assuming requestOptions is an array of objects representing requests
        },
        error => {
          console.error('Error fetching requests:', error);
        }
      );
    }
  }
}

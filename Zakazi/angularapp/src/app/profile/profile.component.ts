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
  requestOptionsClientPending: any[] = [];
  requestOptionsWorker: any[] = [];
  jwtHelper: JwtHelperService;
  constructor(private http: HttpClient, private router: Router) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.fetchAllRequestsClientPending();
    this.fetchAllRequestsWorker();
  }

  onEditOfferClick() {
    this.router.navigate(["offers/edit"]);
  }

  onDeleteOfferClick() {
    // Implement delete offer functionality if needed
  }

  onCancelOfferClick(requestId: number) {
    this.http.delete<any>(`https://localhost:7200/api/Requests/${requestId}`, {}).subscribe(
      (response) => {
        console.log('Request cancelled successfully:', response);
        this.fetchAllRequestsClientPending();
      },
      (error) => {
        console.error('Error cancelling request:', error);
      }
    );
  }

  isAdmin: boolean = false;

  fetchAllRequestsClientPending(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;
      console.log(userId);

      this.http.get<any[]>(`https://localhost:7200/api/Requests/client/${userId}/requests`).subscribe(
        (requests: any[]) => {
          this.requestOptionsClientPending = requests; // Assuming requestOptions is an array of objects representing requests
        },
        error => {
          console.error('Error fetching requests:', error);
        }
      );
    }
  }

  fetchAllRequestsWorker(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;
      console.log(userId);

      this.http.get<any[]>(`https://localhost:7200/api/Requests/worker/${userId}/requests`).subscribe(
        (requests: any[]) => {
          this.requestOptionsWorker = requests; // Assuming requestOptions is an array of objects representing requests
        },
        error => {
          console.error('Error fetching requests:', error);
        }
      );
    }
  }
}

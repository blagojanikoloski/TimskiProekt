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
  postOptionsWorker: any[] = [];
  jwtHelper: JwtHelperService;
  constructor(private http: HttpClient, private router: Router) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.fetchAllRequestsClientPending();
    this.fetchAllRequestsWorker();
    this.fetchAllPostsWorker();
  }

  editOffer(id : number) {
    this.router.navigate(["offers/edit", id]);
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

  deleteOffer(id: number) {
    this.http.delete<any>(`https://localhost:7200/api/Posts/${id}`, {}).subscribe(
      (response) => {
        console.log('Request cancelled successfully:', response);
        this.fetchAllPostsWorker();
        this.fetchAllRequestsWorker();
      },
      (error) => {
        console.error('Error cancelling request:', error);
      }
    );
  }


  acceptRequest(requestId: number) {
    this.http.put(`https://localhost:7200/api/Requests/accept/${requestId}`, {}).subscribe(
      () => {
        console.log('Request accepted successfully.');
        this.fetchAllRequestsWorker();
        // You can add any additional logic here, such as updating UI or reloading data.
      },
      (error) => {
        console.error('Error accepting request:', error);
        // Handle error
      }
    );
  }

  declineRequest(requestId: number) {
    this.http.put(`https://localhost:7200/api/Requests/decline/${requestId}`, {}).subscribe(
      () => {
        console.log('Request declined successfully.');
        this.fetchAllRequestsWorker();
        // You can add any additional logic here, such as updating UI or reloading data.
      },
      (error) => {
        console.error('Error declining request:', error);
        // Handle error
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

  fetchAllPostsWorker(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;
      

      this.http.get<any[]>(`https://localhost:7200/api/Posts/GetMyPosts/${userId}`).subscribe(
        (posts: any[]) => {
          this.postOptionsWorker = posts; // Assuming requestOptions is an array of objects representing requests
          console.log('Posts:' + this.postOptionsWorker);
        },
        error => {
          console.error('Error fetching requests:', error);
        }
      );
    }
  }


  isRequestExpired(request: any): boolean {
    const currentTime = new Date();
    const requestToTime = new Date(request.to);
    return requestToTime < currentTime;
  }
}

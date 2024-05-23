import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {

  profileData: any;
  resultMessage = '';

  editForm!: FormGroup;
  isEditing: boolean = false;


  requestOptionsClientPending: any[] = [];
  requestOptionsWorker: any[] = [];
  postOptionsWorker: any[] = [];
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
  }

  ngOnInit(): void {
    this.fetchAllRequestsClientPending();
    this.fetchAllRequestsWorker();
    //this.fetchAllPostsWorker();
    this.getProfileData();
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
    });
  }

  editOffer(id : number) {
    this.router.navigate(["offers/edit", id]);
  }


  getProfileData(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;

      this.http.get<any>(`https://localhost:7200/api/Account/profile/${userId}`).subscribe(
        (response) => {
          this.profileData = response;
          console.log('Profile Data:', this.profileData);

          // Prefill the form with fetched profile data
          this.editForm.patchValue({
            name: this.profileData.name,
            surname: this.profileData.surname,
            email: this.profileData.email,
            phoneNumber: this.profileData.phoneNumber
          });
        },
        (error) => {
          console.error('Error fetching profile data:', error);
        }
      );
    }
  }

  submitEdit(): void {

    if (this.editForm.valid) {

      const userString = localStorage.getItem('user');
      if (userString) {
        const user = JSON.parse(userString);
        const token = this.jwtHelper.decodeToken(user);
        const userId: number = +token.nameid;

        const editedProfile = this.editForm.value;
        console.log('Payload:', editedProfile);

        // Assuming you're using HttpClient from '@angular/common/http'
        this.http.put(`https://localhost:7200/api/Account/edit-profile/${userId}`, editedProfile).subscribe(
          () => {
            console.log('Profile updated successfully');
            // Optionally, you can reload the profile data after update
            this.getProfileData();
          },
          error => {
            console.error('Error updating profile:', error);
          }
        );
      }
    }
  }

  cancelEditing(): void {
    this.isEditing = false;
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
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

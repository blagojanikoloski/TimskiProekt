import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BusinessService } from '../services/business';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-business-form',
  templateUrl: './business-form.component.html',
  styleUrls: ['./business-form.component.css']
})
export class BusinessFormComponent {
  businessForm = this.fb.group({
    businessName: ['', Validators.required],
  });


  constructor(private fb: FormBuilder, private businessService: BusinessService, private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
  }

  onSubmit() {
    if (this.businessForm.valid) {
      const userString = localStorage.getItem('user');
      if (userString) {
        const user = JSON.parse(userString);
        const token = this.jwtHelper.decodeToken(user);
        const userId: number = +token.nameid; // Parse nameid to number
        console.log('User ID:', userId); // Log the user ID as a number

        console.log('Business Form Data:', this.businessForm.value); // Log the form data

        this.http.post<any>(`https://localhost:7200/api/Business/business`, { ownerId: userId, businessName: this.businessForm.value.businessName })
          .subscribe(
            (response) => {
              console.log('Business created successfully:', response);
              // Optionally, you can redirect to another page or perform other actions upon successful creation
              this.router.navigate(['/home']);
            },
            (error) => {
              console.error('Error creating business:', error);
            }
          );

      } else {
        console.error('User data not found in local storage.');
      }
    } else {
      console.error('Form is invalid.');
    }
  }

}

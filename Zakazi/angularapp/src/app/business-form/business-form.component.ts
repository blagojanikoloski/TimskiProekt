import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BusinessService } from '../services/business';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-business-form',
  templateUrl: './business-form.component.html',
  styleUrls: ['./business-form.component.css']
})
export class BusinessFormComponent {
  businessForm = this.fb.group({
    businessName: ['', Validators.required],
    ownerId: [null, Validators.required],
  });

  jwtHelper: JwtHelperService; // Declare jwtHelper property

  constructor(private fb: FormBuilder, private businessService: BusinessService) {
    this.jwtHelper = new JwtHelperService(); // Initialize jwtHelper
  }

  onSubmit() {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user); // Decode the token
      console.log(token);
    } else {
      console.error('User data not found in local storage.');
    }
  }
}

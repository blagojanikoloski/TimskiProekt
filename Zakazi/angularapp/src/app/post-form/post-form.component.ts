import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {
  postForm!: FormGroup;
  businessOptions: { businessId: number, businessName: string }[] = []; // Define businessOptions as an array of objects

  jwtHelper: JwtHelperService;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.jwtHelper = new JwtHelperService();
  }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      businessId: [null],
      nameOfService: ['', Validators.required],
      price: [0, Validators.required],
      availabilityFrom: [null, Validators.required],
      availabilityTo: [null, Validators.required],
    });

    this.fetchBusinesses();
  }

  onSubmit(): void {
    if (this.postForm.valid) {
      console.log('Form submitted:', this.postForm.value);
    }
  }

  fetchBusinesses(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;

      this.http.get<any[]>(`https://localhost:7200/api/business/owner/${userId}/businesses`).subscribe(
        (businesses: any[]) => {
          this.businessOptions = businesses; // Assuming businesses is an array of objects with businessId and businessName properties
        },
        error => {
          console.error('Error fetching businesses:', error);
        }
      );
    }
  }
}

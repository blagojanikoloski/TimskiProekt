import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {
  postForm!: FormGroup;
  businessOptions: { businessId: number, businessName: string }[] = []; // Define businessOptions as an array of objects

  jwtHelper: JwtHelperService;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
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
      const formData = {
        nameOfService: this.postForm.get('nameOfService')!.value,
        price: this.postForm.get('price')!.value,
        availabilityFrom: this.postForm.get('availabilityFrom')!.value,
        availabilityTo: this.postForm.get('availabilityTo')!.value,
        businessId: this.postForm.get('businessId')!.value
      };

      this.http.post('https://localhost:7200/api/posts', formData).subscribe(
        (response) => {
          console.log('Post created successfully:', response);
          // Optionally, you can reset the form after successful submission
          this.postForm.reset();
          this.router.navigate(['/home']);
        },
        (error) => {
          console.error('Error creating post:', error);
        }
      );
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

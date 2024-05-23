import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'my-businesses',
  templateUrl: 'my-businesses.component.html',
  styleUrls: ['my-businesses.component.css'],
})
export class MyBusinessesComponent {

  businesses: { businessId: number, businessName: string }[] = []; // Define businessOptions as an array of objects

  constructor(private title: Title, private meta: Meta, private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
    this.title.setTitle('MyBusinesses - Zakazi')
    this.meta.addTags([
      {
        property: 'og:title',
        content: 'MyBusinesses - Zakazi',
      },
    ])
  }

  ngOnInit(): void {
  
    this.fetchBusinesses();
  }


  fetchBusinesses(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);
      const userId: number = +token.nameid;

      this.http.get<any[]>(`https://localhost:7200/api/business/owner/${userId}/businesses`).subscribe(
        (businesses: any[]) => {
          this.businesses = businesses; // Assuming businesses is an array of objects with businessId and businessName properties
        },
        error => {
          console.error('Error fetching businesses:', error);
        }
      );
    }
  }

  viewServices(businessId: number): void {
    this.router.navigate(['/business-services', businessId]);
  }
}

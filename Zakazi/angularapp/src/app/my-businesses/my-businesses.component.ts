import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Business, WebApiClient } from '../services/web-api-client.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'my-businesses',
  templateUrl: 'my-businesses.component.html',
  styleUrls: ['my-businesses.component.css'],
})
export class MyBusinessesComponent {

  businesses: Business[] = []; // Define businessOptions as an array of objects

  constructor(private title: Title, private meta: Meta, private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService, private webApiClient: WebApiClient) {
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


  async fetchBusinesses(){
    this.businesses = await lastValueFrom(this.webApiClient.business_GetCurrentUserBusinesses());
  }

  viewServices(businessId: number): void {
    this.router.navigate(['/business-services', businessId]);
  }

  deleteBusiness(businessId: number): void {
    if (confirm('Are you sure you want to delete this post?')) {
      this.http.delete<any[]>(`https://localhost:7200/api/Business/${businessId}`).subscribe(
        () => {
          // Remove the deleted business from the list of businesses
          this.businesses = this.businesses.filter(business => business.businessId !== businessId);
          console.log('Business deleted successfully');
        },
        error => {
          console.error('Error deleting business:', error);
          // Handle error here
        }
      );
    }


  }
}

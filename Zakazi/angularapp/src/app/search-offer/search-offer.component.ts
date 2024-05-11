import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { WebApiClient } from '../services/web-api-client.service';

@Component({
  selector: 'app-search-offer',
  templateUrl: './search-offer.component.html',
  styleUrls: ['./search-offer.component.css'],
})
export class SearchOfferComponent {
  searchParams: any = {};
  responseMessage: string = '';

  constructor(private router: Router, private http: HttpClient) { }

  onEditOfferClick() {
    this.router.navigate(["offers/edit"]);
  }

  onDeleteOfferClick() {
    // Implement delete API call here
  }

  onSearchClick() {
    // Call the API endpoint
    this.http.get<string>('https://localhost:7200/api/Posts/Dummy').subscribe(
      (response) => {
        // Handle the response
        this.responseMessage = response;
        console.log('Response:', response);
      },
      (error) => {
        // Handle errors
        console.error('Error occurred:', error);
      }
    );
  }

  isAdmin: boolean = false;
}

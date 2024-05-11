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
    // Define start and end timestamps
    const startTimestamp = new Date("2022-05-11T08:00:00.000Z");
    const endTimestamp = new Date("2025-05-11T17:00:00.000Z");

    // Format timestamps to the specified format
    const formattedStartTimestamp = startTimestamp.toISOString().replace('T', ' ').split('.')[0];
    const formattedEndTimestamp = endTimestamp.toISOString().replace('T', ' ').split('.')[0];

    // Call the API endpoint with formatted timestamps
    this.http.get<string>(`https://localhost:7200/api/Posts/BetweenTimestamps?startTimestamp=${formattedStartTimestamp}&endTimestamp=${formattedEndTimestamp}`).subscribe(
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

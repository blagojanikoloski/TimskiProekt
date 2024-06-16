import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

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
    // Extract start and end timestamps from searchParams
    const startTimestamp = new Date(this.searchParams.startTimestamp);
    const endTimestamp = new Date(this.searchParams.endTimestamp);

    // Format timestamps to the specified format
    // const formattedStartTimestamp = startTimestamp.toISOString().replace('T', ' ').split('.')[0];
    // const formattedEndTimestamp = endTimestamp.toISOString().replace('T', ' ').split('.')[0];


    // Store timestamps in localStorage
    localStorage.setItem('startTimestamp', startTimestamp.toISOString());
    localStorage.setItem('endTimestamp', endTimestamp.toISOString());
    // Call the API endpoint with formatted timestamps
    this.http.get<any[]>(`https://localhost:7200/api/Business/businesses`).subscribe(
      (response) => {
        // Handle the response
        this.responseMessage = JSON.stringify(response);

        console.log('Response:', response);

        // Serialize the response objects to JSON strings
        const serializedResponse = JSON.stringify(response);

        // Navigate to "offers" view with serialized search result as parameter
        this.router.navigate(['offers'], { queryParams: { searchResult: serializedResponse } });
      },
      (error) => {
        // Handle errors
        console.error('Error occurred:', error);
      }
    );
  }


  isAdmin: boolean = false;
}

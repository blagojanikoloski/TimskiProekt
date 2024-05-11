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

  constructor(private router: Router, private http: HttpClient) { }

  onEditOfferClick() {
    this.router.navigate(["offers/edit"]);
  }

  onDeleteOfferClick() {
    // Implement delete API call here
  }

  onSearchClick() {
    console.log(this.searchParams);
    this.http.post<any>('your-backend-search-api-url', this.searchParams).subscribe(response => {
      // Handle response from backend if needed
    }, error => {
      console.error('Error searching:', error);
      // Handle error if needed
    });
  }

  isAdmin: boolean = false;
}

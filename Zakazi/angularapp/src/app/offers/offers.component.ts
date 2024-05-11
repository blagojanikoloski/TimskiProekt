import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// Define the interface for Offer
interface Offer {
  postId: number;
  businessId: number;
  nameOfService: string;
  price: number;
  availabilityFrom: string;
  // Add other properties if needed
}

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {
  searchResult: Offer[] = []; // Initialize searchResult as an empty array of Offer objects

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    // Retrieve serialized search result from route parameters
    this.route.queryParams.subscribe(params => {
      const serializedResult = params['searchResult'];
      if (serializedResult) {
        // Deserialize the serialized result back to objects
        this.searchResult = JSON.parse(serializedResult) as Offer[];
        console.log(this.searchResult);
      }
    });
  }
}

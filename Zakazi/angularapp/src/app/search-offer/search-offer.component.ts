import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-offer',
  templateUrl: './search-offer.component.html',
  styleUrls: ['./search-offer.component.css'],
})
export class SearchOfferComponent {
  constructor(private router: Router) { }

  onEditOfferClick() {
    this.router.navigate(["offers/edit"])
  }
  onDeleteOfferClick() {
    //delete api call
  }
  isAdmin: boolean = false;
}

import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent {
  constructor(private router: Router) {}
  onLoginClick() {
    this.router.navigate(['/login']);
  }
  onRegisterClick() {
    this.router.navigate(['/register']);
  }
  onAppointmentClick() {
    this.router.navigate(['/search-offer']);
  }
}

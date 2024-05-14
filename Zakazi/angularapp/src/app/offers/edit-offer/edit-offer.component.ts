import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-edit-offer',
  templateUrl: './edit-offer.component.html',
  styleUrls: ['./edit-offer.component.css']
})
export class EditOfferComponent {
  businessOptions: { businessId: number, businessName: string }[] = [];
  postId!: number;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private route: ActivatedRoute, private jwtHelper: JwtHelperService) {
  }

  ngOnInit(): void {

    this.fetchBusinesses();
    
  }

  fetchBusinesses(): void {
    this.route.params.subscribe(params => {
      this.postId = +params['postId']; // (+) converts string 'postId' to a number
      console.log(this.postId);
    });

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

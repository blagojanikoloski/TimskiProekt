import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BusinessService } from '../services/business';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BusinessCreationDto, WebApiClient } from '../services/web-api-client.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-business-form',
  templateUrl: './business-form.component.html',
  styleUrls: ['./business-form.component.css'],
})
export class BusinessFormComponent implements OnDestroy{
  businessSubscription!: Subscription;

  businessForm = this.fb.group({
    businessName: ['', Validators.required],
    businessType: ['', Validators.required],
    imageUrl: [''],
  });

  constructor(
    private fb: FormBuilder,
    private businessService: BusinessService,
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private webApiClient: WebApiClient
  ) {}

  async onSubmit() {
    if (this.businessForm.valid) {
      var businessCreationDto = new BusinessCreationDto();
        businessCreationDto.businessName = this.businessForm.value.businessName!;
        businessCreationDto.imageUrl = this.businessForm.value.imageUrl!;
        this.businessSubscription = await this.webApiClient.business_CreateBusiness(businessCreationDto).subscribe(
        (response) =>{
          console.log('Business created successfully:', response);
          this.router.navigate(['/home']);
        },
        (error) => {
          console.error('Error creating business:', error);
        }
      );
    } else {
      console.error('Form is invalid.');
    }
  }

  ngOnDestroy(): void {
    if(this.businessSubscription){
      this.businessSubscription.unsubscribe();
    }
  }
}

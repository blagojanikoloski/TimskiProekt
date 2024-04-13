import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BusinessService } from '../services/business';

@Component({
  selector: 'app-business-form',
  templateUrl: './business-form.component.html',
  styleUrls: ['./business-form.component.css']
})
export class BusinessFormComponent {
  businessForm = this.fb.group({
    businessName: ['', Validators.required],  
    ownerId: [null, Validators.required],
  });

  constructor(private fb: FormBuilder, private businessService: BusinessService) { }

  onSubmit() {
    if (this.businessForm.valid) {
      this.businessService.createBusiness(this.businessForm.value).subscribe({
        next: (response) => console.log('Business created', response),
        error: (error) => console.error('Error creating business', error)
      });
    }
  }
}

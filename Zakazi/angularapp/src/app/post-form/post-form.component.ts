import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BusinessService } from '../services/business'; 

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent {
  postForm: FormGroup;

  constructor(private fb: FormBuilder, private businessService: BusinessService) {
    this.postForm = this.fb.group({
      businessId: [null, Validators.required],
      nameOfService: ['', Validators.required],
      price: [0, Validators.required],
      availabilityFrom: [null, Validators.required],
      availabilityTo: [null, Validators.required],
    });
  }

  onSubmit(): void {
    if (this.postForm.valid) {
      this.businessService.createPost(this.postForm.value).subscribe({
        next: (response) => console.log('Post created', response),
        error: (error) => console.error('Error creating post', error)
      });
    }
  }
}

import { Title, Meta } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Business, WebApiClient } from '../services/web-api-client.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'business-services',
  templateUrl: 'business-services.component.html',
  styleUrls: ['business-services.component.css'],
})
export class BusinessServicesComponent implements OnInit {
  businessId: number;
  posts: any[] = [];
  isEditing: boolean = false;
  editForm: FormGroup;
  businessOptions: Business[] = [];

  constructor(
    private title: Title,
    private meta: Meta,
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private fb: FormBuilder,
    private webApiClient: WebApiClient
  ) {
    this.title.setTitle('BusinessServicesComponent - Zakazi');
    this.meta.addTags([
      {
        property: 'og:title',
        content: 'BusinessServicesComponent - Zakazi',
      },
    ]);
    this.businessId = 0;
    this.editForm = this.fb.group({
      postId: [null], // Ensure postId is included
      businessId: [null, Validators.required],
      nameOfService: ['', Validators.required],
      price: [0, Validators.required],
    });
  }

  ngOnInit(): void {
    this.businessId = +this.route.snapshot.paramMap.get('id')!;
    this.fetchPosts();
    this.fetchBusinesses();
  }

  fetchPosts(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      const token = this.jwtHelper.decodeToken(user);

      this.http.get<any[]>(`https://localhost:7200/api/Posts/ByBusiness/${this.businessId}`).subscribe(
        (posts: any[]) => {
          this.posts = posts;
        },
        error => {
          console.error('Error fetching posts:', error);
        }
      );
    }
  }

  deletePost(postId: number): void {
    if (confirm('Are you sure you want to delete this post?')) {
      this.http.delete(`https://localhost:7200/api/Posts/${postId}`).subscribe(
        () => {
          this.posts = this.posts.filter(post => post.postId !== postId);
        },
        error => {
          console.error('Error deleting post:', error);
        }
      );
    }
  }

  startEditing(post: any): void {
    this.isEditing = true;
    this.editForm.patchValue({
      postId: post.postId,
      businessId: post.businessId,
      nameOfService: post.nameOfService,
      price: post.price
    });
  }

  cancelEditing(): void {
    this.isEditing = false;
    this.editForm.reset();
  }

  submitEdit(): void {
    if (this.editForm.valid) {
      const editedPost = this.editForm.value;
      console.log('Payload:', editedPost); 

      this.http.put(`https://localhost:7200/api/Posts/${editedPost.postId}`, editedPost).subscribe(
        () => {
          this.fetchPosts();
          this.isEditing = false;
        },
        error => {
          console.error('Error updating post:', error);
        }
      );
    }
  }

  async fetchBusinesses(){
   this.businessOptions = await lastValueFrom(this.webApiClient.business_GetCurrentUserBusinesses());
  }
}

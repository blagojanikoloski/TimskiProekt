import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  constructor(private http: HttpClient) { }

  createBusiness(business: any): Observable<any> {
    return this.http.post('/api/business', business);
  }

  createPost(post: any): Observable<any> {
    return this.http.post('/api/posts', post);
  }
}

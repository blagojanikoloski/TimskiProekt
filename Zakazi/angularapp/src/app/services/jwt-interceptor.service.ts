import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class JwtInterceptor implements HttpInterceptor {
  originalRequestFailed = false;

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      // add auth header with jwt if account is logged in and request is to the api url
      const authToken = localStorage.getItem('user');
      const isApiUrl = request.url.startsWith(environment.apiUrl);
      if (authToken && isApiUrl) {
          request = request.clone({
              setHeaders: { Authorization: `Bearer ${authToken}` }
          });
      }
      return next.handle(request);
  }
}
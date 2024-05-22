import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomePageComponent } from './home-page/home-page.component';
import { API_BASE_URL, WebApiClient } from './services/web-api-client.service';
import { environment } from '../environments/environment';
import { SharedModule } from './shared/shared.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { OffersComponent } from './offers/offers.component';
import { MyBusinessesComponent } from './my-businesses/my-businesses.component';
import { BusinessServicesComponent } from './business-services/business-services.component';
import { SearchOfferComponent } from './search-offer/search-offer.component';
import { EditOfferComponent } from './offers/edit-offer/edit-offer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BusinessFormComponent } from './business-form/business-form.component';
import { PostFormComponent } from './post-form/post-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

export function tokenGetter() {
  const token = localStorage.getItem('user');
  return token ? JSON.parse(token) : 'User Not Logged In';
}

@NgModule({
  declarations: [AppComponent, HomePageComponent, LoginComponent, RegisterComponent, ProfileComponent, OffersComponent, MyBusinessesComponent, BusinessServicesComponent , SearchOfferComponent, EditOfferComponent, BusinessFormComponent, PostFormComponent],
  imports: [BrowserModule, HttpClientModule, NgbModule, SharedModule, BrowserAnimationsModule, FormsModule,ReactiveFormsModule, 
    JwtModule.forRoot({
    config: {
      tokenGetter: tokenGetter,
      disallowedRoutes: [],
    },
  }),],
  providers: [
    WebApiClient,
    { provide: API_BASE_URL, useValue: environment.apiUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

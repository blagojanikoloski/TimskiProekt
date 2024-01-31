import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomePageComponent } from './home-page/home-page.component';
import { API_BASE_URL, WebApiClient } from './services/web-api-client.service';
import { environment } from './environments/environment';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent, HomePageComponent],
  imports: [BrowserModule, HttpClientModule, NgbModule, SharedModule],
  providers: [
    WebApiClient,
    { provide: API_BASE_URL, useValue: environment.apiUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

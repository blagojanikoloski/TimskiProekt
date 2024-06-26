import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { OffersComponent } from './offers/offers.component';
import { EditOfferComponent } from './offers/edit-offer/edit-offer.component';
import { BusinessFormComponent } from './business-form/business-form.component';
import { SearchOfferComponent } from './search-offer/search-offer.component';
import { MyBusinessesComponent } from './my-businesses/my-businesses.component';
import { BusinessServicesComponent } from './business-services/business-services.component';
import { PostFormComponent } from './post-form/post-form.component';
import { BusinessService } from './services/business';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomePageComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
  },
  {
    path: 'offers',
    children:[
      {
        path: '',
        component: OffersComponent,
      },
      {
        path: 'edit',
        component: EditOfferComponent,
      },
    ]
  },
  { path: 'offers/edit/:postId', component: EditOfferComponent },
  {
    path: 'business-form',
    component: BusinessFormComponent,
  },
  {
    path: 'post-form',
    component: PostFormComponent,
  },
  {
    path: 'search-offer', 
    component: SearchOfferComponent, 
  },
  {
    path: 'my-businesses',
    component: MyBusinessesComponent,
  },
  {
    path: 'business-services/:id',
    component: BusinessServicesComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

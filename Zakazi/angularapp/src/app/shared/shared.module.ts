import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from '../app-routing.module';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';



@NgModule({
  declarations: [
    LoadingSpinnerComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ],
  exports:[
    AppRoutingModule,
    LoadingSpinnerComponent
  ]
})
export class SharedModule { }

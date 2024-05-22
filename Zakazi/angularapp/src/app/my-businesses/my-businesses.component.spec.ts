import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'
import { RouterModule } from '@angular/router'
import { CommonModule } from '@angular/common'

import { ComponentsModule } from '../components/components.module'
import { MyBusinessesComponent } from './my-businesses.component'

const routes = [
  {
    path: '',
    component: MyBusinessesComponent,
  },
]

@NgModule({
  declarations: [MyBusinessesComponent],
  imports: [CommonModule, ComponentsModule, RouterModule.forChild(routes)],
  exports: [MyBusinessesComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class MyBusinessesModule {}

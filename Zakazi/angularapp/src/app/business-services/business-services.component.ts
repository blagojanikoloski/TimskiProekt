import { Component } from '@angular/core'
import { Title, Meta } from '@angular/platform-browser'

@Component({
  selector: 'business-services',
  templateUrl: 'business-services.component.html',
  styleUrls: ['business-services.component.css'],
})
export class BusinessServicesComponent {
  constructor(private title: Title, private meta: Meta) {
    this.title.setTitle('BusinessServicesComponent - Zakazi')
    this.meta.addTags([
      {
        property: 'og:title',
        content: 'BusinessServicesComponent - Zakazi',
      },
    ])
  }
}

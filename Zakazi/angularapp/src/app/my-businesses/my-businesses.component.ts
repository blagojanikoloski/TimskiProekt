import { Component } from '@angular/core'
import { Title, Meta } from '@angular/platform-browser'

@Component({
  selector: 'my-businesses',
  templateUrl: 'my-businesses.component.html',
  styleUrls: ['my-businesses.component.css'],
})
export class MyBusinessesComponent {
  constructor(private title: Title, private meta: Meta) {
    this.title.setTitle('MyBusinesses - Zakazi')
    this.meta.addTags([
      {
        property: 'og:title',
        content: 'MyBusinesses - Zakazi',
      },
    ])
  }
}

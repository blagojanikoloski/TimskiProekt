import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchOfferComponent } from './search-offer.component';

describe('SearchofferComponent', () => {
  let component: SearchOfferComponent;
  let fixture: ComponentFixture<SearchOfferComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchOfferComponent]
    });
    fixture = TestBed.createComponent(SearchOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

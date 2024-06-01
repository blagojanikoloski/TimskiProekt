import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessServicesComponent } from './business-services.component';

describe('HomePageComponent', () => {
  let component: BusinessServicesComponent;
  let fixture: ComponentFixture<BusinessServicesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BusinessServicesComponent]
    });
    fixture = TestBed.createComponent(BusinessServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBusinessesComponent } from './my-businesses.component';

describe('MyBusinessesComponent', () => {
  let component: MyBusinessesComponent;
  let fixture: ComponentFixture<MyBusinessesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyBusinessesComponent]
    });
    fixture = TestBed.createComponent(MyBusinessesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

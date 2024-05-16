import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyListingsComponent } from './property-listings.component';

describe('PropertyListingsComponent', () => {
  let component: PropertyListingsComponent;
  let fixture: ComponentFixture<PropertyListingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertyListingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PropertyListingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

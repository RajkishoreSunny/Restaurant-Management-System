import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantmanagerComponent } from './restaurantmanager.component';

describe('RestaurantmanagerComponent', () => {
  let component: RestaurantmanagerComponent;
  let fixture: ComponentFixture<RestaurantmanagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RestaurantmanagerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RestaurantmanagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenucategoryComponent } from './menucategory.component';

describe('MenucategoryComponent', () => {
  let component: MenucategoryComponent;
  let fixture: ComponentFixture<MenucategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MenucategoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MenucategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

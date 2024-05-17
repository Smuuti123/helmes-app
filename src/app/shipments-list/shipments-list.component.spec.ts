import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipmentsListComponent } from './shipments-list.component';

describe('ShipmentsListComponent', () => {
  let component: ShipmentsListComponent;
  let fixture: ComponentFixture<ShipmentsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShipmentsListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShipmentsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

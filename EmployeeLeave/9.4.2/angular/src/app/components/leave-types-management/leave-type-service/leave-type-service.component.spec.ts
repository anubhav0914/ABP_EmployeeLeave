import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveTypeServiceComponent } from './leave-type-service.component';

describe('LeaveTypeServiceComponent', () => {
  let component: LeaveTypeServiceComponent;
  let fixture: ComponentFixture<LeaveTypeServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveTypeServiceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveTypeServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardFounderComponent } from './dashboard.component';

describe('DashboardComponent', () => {
  let component: DashboardFounderComponent;
  let fixture: ComponentFixture<DashboardFounderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardFounderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardFounderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

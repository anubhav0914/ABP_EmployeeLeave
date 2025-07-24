import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserNotificationComponent } from '@app/components/user-notification/user-notification.component';
import { CommonModule } from '@node_modules/@angular/common';
import {EmployeeNotificationComponent} from "../../employee/employee-notification/employee-notification.component"

@Component({
  selector: 'app-dashboard',
  standalone:true,
  templateUrl: './dashboard.component.html',
  imports : [CommonModule,UserNotificationComponent,EmployeeNotificationComponent]
})
export class DashboardFounderComponent {
  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}

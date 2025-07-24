import { Component, OnInit, NgZone, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { LeaveRequestServices } from '../../services/leave-request-services';
import { jwtDecode } from '@node_modules/jwt-decode/build/cjs';
import { CommonModule, Location } from '@node_modules/@angular/common';
import { EmployeeNotificationComponent } from '../employee/employee-notification/employee-notification.component';

@Component({
  selector: 'app-employee-profile',
  templateUrl: './employee-profile.component.html',
  standalone: true,
  styleUrls: ['./employee-profile.component.scss'],
  imports: [CommonModule,EmployeeNotificationComponent]
})
export class EmployeeProfileComponent implements OnInit {
  employee: any | null = null;
  leaveRequests: any[] = [];
  loading: boolean = true;
  showLeaveRequests: boolean = false;

  constructor(
    private router: Router,
    private employeeService: EmployeeService,
    private leaveRequestService: LeaveRequestServices,
    private location: Location,
    private ngZone: NgZone,
    private cdRef: ChangeDetectorRef // 👈 inject ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.fetchEmployeeData();
  }

  fetchEmployeeData(): void {
    const token = abp.auth.getToken();
    const decoded: any = jwtDecode(token);
    const id = Number(decoded["sub"]);

    this.employeeService.getEmployeeByUserId(id).subscribe({
      next: (res) => {
        this.employee = res.result.data;
        this.loading = false;
        this.cdRef.detectChanges(); // 👈 force change detection
        this.loadLeaveRequests(); // make sure this is after employee is set
      },
      error: (err) => {
        console.error('Failed to load employee data:', err);
        this.router.navigate(['/dashboard/employee']);
      }
    });
  }

  goBack(): void {
    this.location.back();
  }

  applyForLeave(): void {
    this.router.navigate(['app/apply/leave'], { state: { employee: this.employee } });
  }

  loadLeaveRequests(): void {
    this.leaveRequestService.getAll().subscribe({
      next: (res) => {
        this.leaveRequests = res.result?.data.filter(req => req.employeeId === this.employee?.id);
        this.showLeaveRequests = true;
        this.cdRef.detectChanges(); // 👈 also ensure this is detected
      },
      error: (err) => {
        console.error('Failed to load leave requests:', err);
      }
    });
  }
}

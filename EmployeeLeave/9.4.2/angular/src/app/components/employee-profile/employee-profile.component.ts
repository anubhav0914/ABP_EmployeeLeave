import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeResponseDto } from '../../shared/service-proxies/employee/model/employeeResponseDto';
import { EmployeeService } from '../../services/employee.service';
import { LeaveRequestServices } from '../../services/leave-request-services';
import { LeaveRequestCreateUpdateDtoApiResponse } from '../../shared/service-proxies/employee/model/leaveRequestCreateUpdateDtoApiResponse';
import { jwtDecode } from '@node_modules/jwt-decode/build/cjs';
import { CommonModule } from '@node_modules/@angular/common';
import { Location } from '@node_modules/@angular/common';

@Component({
  selector: 'app-employee-profile',
  templateUrl: './employee-profile.component.html',
  standalone:true,
  styleUrls: ['./employee-profile.component.scss'],
  imports :[CommonModule]
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
    private location : Location
  ) {}

  ngOnInit(): void {
    this.fetchEmployeeData();
    this.loadLeaveRequests();
  }

  fetchEmployeeData(): void {
    const toekn = abp.auth.getToken();
    const decode = jwtDecode(toekn);
    console.log(decode)
    const id =  Number(decode["sub"])
    console.log(id)
    this.employeeService.getEmployeeByUserId(id).subscribe({
      next: (res) => {
        this.employee = res.result.data;
        console.log(this.employee)
        this.loading = false;
      },
      error: (err) => {
        console.error('Failed to load employee data:', err);
        this.router.navigate(['/dashboard/employee']);
      }
    });
  }

  goBack() : void {
    this.location.back();
  }

  applyForLeave(): void {
    this.router.navigate(['app/apply/leave'], { state: { employee: this.employee } });
  }

  loadLeaveRequests(): void {

    this.leaveRequestService.getAll().subscribe({
      next: (res) => {
        console.log("leave request",res)
        this.leaveRequests = res.result?.data.filter(req => req.employeeId === this.employee.id);
        this.showLeaveRequests = true;
      },
      error: (err) => {
        console.error('Failed to load leave requests:', err);
      }
    });
  }
}

import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { EmployeeServicesService } from '../../shared/service-proxies/employee/api/employeeServices.service';
import { EmployeeResponseDto } from '../../shared/service-proxies/employee/model/employeeResponseDto';
import { TokenService } from 'abp-ng2-module';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Location } from '@angular/common';


@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  templateUrl: './employee-dashboard.component.html',
  styleUrls: ['./employee-dashboard.component.scss'],
  imports: [CommonModule]
})
export class EmployeeDashboardComponent implements OnInit {
  employee: EmployeeResponseDto | null = null;
  loading = true;
  error: string | null = null;
  notFound = false; 

  constructor(
    private employeeService: EmployeeServicesService,
    private tokenService: TokenService,
    private router: Router,
    private location: Location,
     private cdRef: ChangeDetectorRef 
  ) {}

 ngOnInit(): void {
  const token = this.tokenService.getToken();
  if (!token) {
    this.error = 'Token not found';
    this.loading = false;
    return;
  }

  try {
    const decoded: any = jwtDecode(token);
    const userId = decoded?.userId || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

    if (!userId) {
      this.error = 'User ID not found in token.';
      this.loading = false;
       this.cdRef.detectChanges(); 
      return;
    }
    

    // ❗ Use `any` temporarily for response type
    this.employeeService.apiServicesAppEmployeeServicesGetEmployeeByUserIdGet(userId).subscribe({
      next: (res: any) => {
        console.log("full response:", res);
        if (res && res.result && res.result.data) {
          this.employee = res.result.data;
        } else {
          this.notFound = true;
        }
        this.loading = false;
        this.cdRef.detectChanges();

      },
      error: (err) => {
        this.error = 'Failed to load employee data.';
        this.loading = false;
         this.cdRef.detectChanges(); 
        console.error(err);
      },
    });
  } catch (e) {
    this.error = 'Error decoding token.';
    this.loading = false;
    console.error(e);
  }
}

goToUserDashboard() {
  const employeeData = { 
    id: this.employee.id, 
    firstName: this.employee.firstName, 
    isApllied : true,
    approvedByFounder: this.employee.isApprovedByFounder 
  };

  this.router.navigate(['app/dashboard/user'], {
    state: { employee: employeeData }
  });
}

goBack(): void {
    this.location.back();
  }

goToProfile(): void {
  this.router.navigate(['app/employee/profile'], { state: { employee: this.employee } });
}


  goToApply(): void {
    this.router.navigate(['register/employee']);
  }
}

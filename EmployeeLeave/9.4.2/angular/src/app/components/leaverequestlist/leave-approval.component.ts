import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LeaveRequestServices } from '../../services/leave-request-services';
import { FounderService } from '../../services/founder-services';
import { ApproveLeaveDto } from '../../shared/service-proxies/employee/model/approveLeaveDto'
import { LeaveStatus } from '../../shared/service-proxies/employee';
import { jwtDecode } from '@node_modules/jwt-decode/build/cjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-leave-approval',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './leave-approval.component.html',
  styleUrls: ['./leave-approval.component.scss'],
})
export class LeaveApprovalComponent implements OnInit {
  leaveRequests: any[] = [];
  userId = 1; // Replace with actual logged-in founder's user ID

  constructor(
    private leaveService: LeaveRequestServices,
    private founderService: FounderService,
    private location : Location
  ) { }

  ngOnInit(): void {
    this.getLeaveRequests();
  }

 getLeaveRequests(): void {
  this.leaveService.getAll().subscribe({
    next: (response) => {
      console.log('full response', response);
      this.leaveRequests = response?.result?.data || [];
    },
    error: (err) => {
      console.error('Failed to fetch leave requests', err);
      this.leaveRequests = [];
    }
  });
}
 
goBack():void{
   this.location.back();
}

  updateLeaveStatus(request: any, status: LeaveStatus): void {
    const token = abp.auth.getToken();
    const decoded =  jwtDecode(token)
    console.log(decoded);
    const userIdcurrnet = Number(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'])
    const dto: ApproveLeaveDto = {
      employeeId: request.employeeId,
      approved_by_ID: userIdcurrnet,
      requestId: request.id,
      status: status,

    };
    
    this.founderService.changestatus(dto).subscribe({
      next: () => {
        console.log("recived resopnse" );
        request.status = status; // Update locally
        this.getLeaveRequests();
      },
      error: (err) => {
        console.error('Failed to update status', err);
      }
    });
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Approved';
      case 2: return 'Rejected';
      default: return 'Unknown';
    }
  }
}

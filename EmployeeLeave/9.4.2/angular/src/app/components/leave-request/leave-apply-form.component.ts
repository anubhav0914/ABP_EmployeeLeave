import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LeaveRequestServices } from '../../services/leave-request-services';
import { LeaveTypeServices } from '../../services/leave-type-service'
import { LeaveRequestCreateUpdateDto } from '../../shared/service-proxies/employee/model/leaveRequestCreateUpdateDto';
import { Router } from '@node_modules/@angular/router';
import { EmployeeService} from '../../services/employee.service';
import { jwtDecode } from '@node_modules/jwt-decode/build/cjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-leave-apply-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './leave-apply-form.component.html',
  styleUrls: ['../../../style.css']
})
export class LeaveApplyFormComponent implements OnInit {
  @Input() employee: any;

  leaveForm!: FormGroup;
  leaveTypes: any[] = [];
  id : number;

  constructor(
    private fb: FormBuilder,
    private leaveTypeService: LeaveTypeServices,
    private leaveRequestService: LeaveRequestServices,
    private employeeService: EmployeeService,
    private location:  Location
  ) {}

  ngOnInit(): void {
    this.getEmployee();
    this.loadLeaveTypes();
    this.initForm();
  }

  initForm() {
    this.leaveForm = this.fb.group({
      leaveTypeId: [null, Validators.required],
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
      reason: ['']
    });
  }
  getEmployee(){
     const token = abp.auth.getToken();
     const decoedToken = jwtDecode(token);
     console.log(decoedToken)
     const id = Number( decoedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] )
     this.employeeService.getEmployeeByUserId(id).subscribe(res=>{
     this.employee = res.result?.data || undefined
     this.id = this.employee?.id
     console.log(this.employee)
     })
  }
  loadLeaveTypes() {
    this.leaveTypeService.getAll().subscribe(res => {
      this.leaveTypes = res.result?.data || [];
    });
  }

  goBack() :void {
    this.location.back();
  }  
  
  onSubmit() {
    if (this.leaveForm.invalid) return;

    const formValues = this.leaveForm.value;

    const payload: LeaveRequestCreateUpdateDto = {
      employeeId: this.id,
      leaveTypeId: Number (formValues.leaveTypeId),
      fromDate: formValues.fromDate,
      toDate: formValues.toDate,
      reason: formValues.reason,
      status: 0 // pending
    };
    console.log(this.employee)
    console.log(payload)
    this.leaveRequestService.create(payload).subscribe(() => {
      alert('Leave applied successfully!');
      this.leaveForm.reset();
    });
  }
}

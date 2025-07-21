import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeaveTypeServices } from '../../../services/leave-type-service'
import { LeaveTypeDto } from '../../../shared/service-proxies/employee/model/leaveTypeDto';
import { CommonModule } from '@node_modules/@angular/common';
import { Location } from '@node_modules/@angular/common';

@Component({
  selector: 'app-leave-type-service',
  standalone: true,
  templateUrl: './leave-type-service.component.html',
  imports: [CommonModule, ReactiveFormsModule]
})
export class LeaveTypeServiceComponent implements OnInit {
  selectedTab: 'create' | 'list' = 'create';
  leaveTypeForm: FormGroup;
  leaveTypes: LeaveTypeDto[] = [];

  constructor(
    private fb: FormBuilder,
    private leaveTypeService: LeaveTypeServices,
    private location:Location
  ) {
    this.leaveTypeForm = this.fb.group({
      name: [null, Validators.required],
      description: [null],
      maxDaysAllowed: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadLeaveTypes();
  }

  loadLeaveTypes(): void {
    this.leaveTypeService.getAll().subscribe((result) => {
      this.leaveTypes = result.result?.data || [];
    });
  }

  goBack(): void{
    this.location.back();
  }
  onSubmit(): void {
    const dto: LeaveTypeDto = {
      name: this.leaveTypeForm.value.name,
      description: this.leaveTypeForm.value.description,
      maxDaysAllowed: this.leaveTypeForm.value.maxDaysAllowed
        ? this.leaveTypeForm.value?.maxDaysAllowed.toString()
        : "1"
    };

    if (dto) {
      this.leaveTypeService.addleave(dto).subscribe(() => {
        alert('Leave type created!');
        this.leaveTypeForm.reset();
        this.loadLeaveTypes();
        this.selectedTab = 'list';
      });
    }
  }
}

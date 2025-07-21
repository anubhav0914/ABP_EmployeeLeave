import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { FounderService } from '../../services/founder-services';
import { CommonModule } from '@angular/common';
import { Location } from '@angular/common';

@Component({
  selector: 'app-employee-with-status',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-with-status.component.html',
  // styleUrls: ['../../../style.css']  // Removed problematic global style
})
export class EmployeeWithStatusComponent implements OnInit {
  allEmployeess: any[] = [];
  approvedEmployees: any[] = [];
  requestedEmployees: any[] = [];
  activeTab: string = 'all';

  constructor(
    private employeeService: EmployeeService,
    private founderService: FounderService,
    private location : Location
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.employeeService.getAll().subscribe(res => {
      this.allEmployeess = res.result?.data;
      console.log("All Employees", this.allEmployeess);
    });

    this.employeeService.getApprovedEmployees().subscribe(res => {
      this.approvedEmployees = res.result?.data;
    });

    this.employeeService.getRequestedEmployees().subscribe(res => {
      this.requestedEmployees = res.result?.data;
    });
  }

  approveEmployee(id: number) {
    this.founderService.approveEmployee(id).subscribe(() => {
      alert('Employee approved!');
      this.loadData();
    });
  }

  goBack() : void {
    this.location.back();
  }
}

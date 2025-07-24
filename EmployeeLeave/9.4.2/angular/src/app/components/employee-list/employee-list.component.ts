import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../services/employee.service';
import { Location } from '@angular/common';
import { EmployeeResponseDto } from '../../shared/service-proxies/employee/model/employeeResponseDto';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss'],
  imports: [CommonModule],
})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeResponseDto[] = [];
  loading = true;
  error: string | null = null;

  constructor(private employeeService: EmployeeService,private location : Location,private chageDetection : ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.fetchEmployees();
    console.log("got data" + this.fetchEmployees())
  }

 fetchEmployees(): void {
  this.loading = true;
  this.employeeService.getAll().subscribe({
    next: (res) => {
      console.log('Full response:', res);
      this.employees = res?.result?.data ?? []; // ✅ Correctly pulling from nested structure
      this.loading = false;
      this.chageDetection.detectChanges();
    },
    error: (err) => {
      this.error = 'Failed to load employee data.';
      this.loading = false;
      this.chageDetection.detectChanges();
      console.error(err);
    }
  });

}
  goBack(): void {
    this.location.back();
}

}

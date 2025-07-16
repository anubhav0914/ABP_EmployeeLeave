// src/app/components/employee-list/employee-list.component.ts
import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { Client, EmployeeResponseDto } from '../../../shared/service-proxies/employee-service-proxy';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-employee-list',
  standalone:true,
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
  imports: [CommonModule],
  providers: [
    {
      provide: Client,
      useFactory: () => new Client('http://localhost:5000')  // 👈 update base URL accordingly
    }
  ]

})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeResponseDto[] = [];
  loading = true;
  error: string | null = null;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.fetchEmployees();
  }

  fetchEmployees(): void {
    this.loading = true;
    this.employeeService.getAll().subscribe({
      next: (res) => {
        this.employees = res.data ?? [];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load employee data.';
        this.loading = false;
        console.error(err);
      }
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TokenService } from '@node_modules/abp-ng2-module'; // adjust this import if using a custom TokenService
import { Router } from '@angular/router';
import {jwtDecode} from 'jwt-decode';
import { ManagerService } from '../../services/manager-servoices'; // update path accordingly
import { CommonModule } from '@node_modules/@angular/common';
import { Location } from '@node_modules/@angular/common';
import { basename } from 'path';

@Component({
  selector: 'app-register-manager',
  templateUrl: './register-manager.component.html',
  styleUrls: ['./register-manager.component.scss'],
  standalone: true,
  imports:[CommonModule,ReactiveFormsModule]
})
export class RegisterManagerComponent implements OnInit {
  managerForm!: FormGroup;
  userIdFromToken: number = 0;

  constructor(
    private fb: FormBuilder,
    private tokenService: TokenService,
    private router: Router,
    private managerService: ManagerService,
    private location : Location
  ) {}

  ngOnInit(): void {
    this.extractUserIdFromToken();

    this.managerForm = this.fb.group({
      dateOfJoining: [null, Validators.required],
      work_experince_year: [0, [Validators.required, Validators.min(0)]],
      isActive: [true]
    });
  }

  extractUserIdFromToken(): void {
    const token = this.tokenService.getToken();
    if (token) {
      const decoded: any = jwtDecode(token);
      this.userIdFromToken = Number(decoded?.userId || decoded?.sub || 0);
    }
  }

  goBack() : void {
    this.location.back();
  }

  onSubmit(): void {
    if (this.managerForm.invalid) return;

    const payload = {
      userId: this.userIdFromToken,
      ...this.managerForm.value
    };

     console.log(payload)
    this.managerService.register(payload).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err) => console.error('Error creating manager:', err)
    });
  }
}

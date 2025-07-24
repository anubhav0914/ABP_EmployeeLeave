import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common'; // if using common directives like *ngIf, etc.
import { Router } from '@angular/router';
import { jwtDecode } from '@node_modules/jwt-decode/build/cjs';
import { EmployeeServicesService } from '@app/shared/service-proxies/employee/api/employeeServices.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-register-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], // ✅ Import modules here
  templateUrl: './register-employee.component.html',
  styleUrls: ['./register-employee.component.scss']
})
export class RegisterEmployeeComponent implements OnInit {
  employeeForm: FormGroup;
  token  = abp.auth.getToken();
  decodedtoken :any  = jwtDecode(this.token)
  emailFromJwt = this.decodedtoken?.emailaddress || this.decodedtoken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
  
// Example (Illustrative)
  constructor(private fb: FormBuilder, private router: Router,private employeeService: EmployeeServicesService,private location:Location ) {}

  ngOnInit(): void {
    console.log(this.decodedtoken)
    this.employeeForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: [{ value: this.emailFromJwt, disabled: true }, [Validators.required, Validators.email]],
      department: ['', Validators.required],
      dateOfJoining: ['', Validators.required]
    });
  }
 navigate(route: string) {
  this.router.navigate([route]);
}
 
  onSubmit(): void {
  if (this.employeeForm.valid) {
    const employeeData = {
      ...this.employeeForm.getRawValue(),
    };

    this.employeeService.apiServicesAppEmployeeServicesRegisterEmployeePost(employeeData).subscribe({
      next: (res) => {
        console.log('Employee registered successfully:', res);
        // ✅ Optionally navigate to a success page or dashboard
        alert("Applied for the employee role ,Please wait Founder will Approve you soon")
        this.router.navigate(['app/dashboard/user']);
      },
      error: (err) => {
        console.error('Error registering employee:', err);
        // ✅ Optionally show a toast or error message
      }
    });
  }
}

goBack():void{
  this.location.back();
}

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TokenService } from 'abp-ng2-module';
import { jwtDecode } from 'jwt-decode';
import { Location } from '@angular/common';

@Component({
  selector: 'app-role-selection',
  standalone: true,
  templateUrl: './role-selection.component.html',
  styleUrls: ['../../../style.css'],
  imports: [CommonModule]
})
export class RoleSelectionComponent implements OnInit {
  userId: number | null = null;
  userName: string = '';
  hasRole: boolean = false;
  employee : any = []

  constructor(
    private router: Router,
    private tokenService: TokenService,
    private location : Location
    
  ) {
    const navigation = this.router.getCurrentNavigation();
    this.employee = navigation?.extras?.state?.['employee'];
    console.log(this.employee)
  }

  ngOnInit(): void {
    if(this.employee !=null){
        this.hasRole = true;
      }
    const token = this.tokenService.getToken();
    if (!token) {
      console.error('Token not found.');
      return;
    }

    try {
     
      const decoded: any = jwtDecode(token);
      this.userId = Number(decoded?.userId) || Number(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
      this.userName = decoded?.userName || decoded?.sub || 'User';

      const roleClaim = decoded['role'] || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      const roles = Array.isArray(roleClaim) ? roleClaim : [roleClaim];
      this.hasRole = roles?.some(role => role && role !== 'Admin'); // Ignore 'Admin' role if needed

    } catch (error) {
      console.error('Failed to decode token', error);
    }
  }

  applyForEmployee(): void {
    this.router.navigate(['app/register/employee'], {
      state: { userId: this.userId, userName: this.userName }
    });
  }
  
  goBack() :void {
    this.location.back();
  }
  applyForManager(): void {
    this.router.navigate(['app/manager/register'], {
      state: { userId: this.userId, userName: this.userName }
    });
  }
}

import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManagerResponseDto } from '../../shared/service-proxies/employee/model/managerResponseDto'
import { ManagerResponseDtoListApiResponse } from '../../shared/service-proxies/employee/model/managerResponseDtoListApiResponse'
import { ManagerService } from '../../services/manager-servoices';
import { jwtDecode } from 'jwt-decode';
import { CommonModule } from '@node_modules/@angular/common';
import { Location } from '@node_modules/@angular/common';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  templateUrl: './manager-dashboard.component.html',
  styleUrls: ['../../../style.css'],
  imports :[CommonModule]
})
export class ManagerDashboardComponent implements OnInit {
  manager: ManagerResponseDto | null = null;
  isApproved: boolean = false;
  loading = true;

  constructor(
    private router: Router,
    private managerService: ManagerService,
    private locatio : Location,
    private chageDetection : ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    const token = abp.auth.getToken(); // ABP token fetch
    if (!token) {
      console.error('Token not found');
      return;
    }

    const decodedToken: any = jwtDecode(token);
    const userId = Number(decodedToken?.sub);

    if (!userId) {
      console.error('User ID not found in token');
      return;
    }

    this.managerService.gellAll().subscribe({
      next: (response: any) => {
        console.log(response)
        const foundManager = response.result?.data.find(m => m.userId === userId);
        console.log("found manager", foundManager) 
        if (foundManager) {
          this.manager = foundManager;
          console.log(this.manager)
          this.isApproved = foundManager.isApproved_by_Founder === true;
          this.loading = false;
          this.chageDetection.detectChanges();
        }
      },
      error: (err) => {
        console.error('Error fetching approved managers:', err);
        this.loading = false;
        this.chageDetection.detectChanges();

      }
    });

  }
  
  goBack():void {
    this.locatio.back();
  }
  goTo(path: string): void {
    this.router.navigate([path]);
  }
}

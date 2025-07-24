import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ManagerService } from '../services/manager-servoices';
import { FounderService } from '../services/founder-services';
import { CommonModule } from '@angular/common';
import { Location } from '@angular/common';
@Component({
  selector: 'app-manager-management',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manager-management.component.html',
  styleUrls: ['../../style.css']
})
export class ManagerManagementComponent implements OnInit {
  allManagers: any[] = [];
  approvedManagers: any[] = [];
  requestedManagers: any[] = [];
  activeTab: string = 'all';

  constructor(
    private managerService: ManagerService,
    private founderService: FounderService,
    private location : Location,
    private changeDetection : ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.managerService.gellAll().subscribe(res => {
      this.allManagers = res.result?.data;
      console.log("all managers", this.allManagers);
      this.changeDetection.detectChanges();
    });

    this.managerService.getApproved().subscribe(res => {
      this.approvedManagers = res.result?.data;
      this.changeDetection.detectChanges();

    });

    this.managerService.getREquested().subscribe(res => {
      this.requestedManagers = res.result?.data;
      this.changeDetection.detectChanges();

    });
  }

  goBack() : void {
    this.location.back();
  }
  approveManager(id: number) {
    console.log(id)
    this.founderService.approveManager(id).subscribe(() => {
      alert('Manager approved!');
      this.loadData();
    });
    this.changeDetection.detectChanges();
  }
}

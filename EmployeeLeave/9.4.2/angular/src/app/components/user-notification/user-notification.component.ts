import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SignalRAspNetCoreHelper } from '../../../shared/helpers/SignalRAspNetCoreHelper';
import { CommonModule } from '@angular/common';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-user-notification',
  templateUrl: './user-notification.component.html',
  standalone: true,
  styleUrl: '../../../style.css',
  imports: [CommonModule]
})
export class UserNotificationComponent implements OnInit {

  messages: string[] = [];
  constructor(private changeDetection : ChangeDetectorRef, private router : Router){
    
  } 

  ngOnInit(): void {
    console.log("Notification is initialized");

    SignalRAspNetCoreHelper.initSignalR();

    abp.event.on('abp.notifications.received', (userNotification) => {
      const message = userNotification.notification.data.properties?.Message;
      if (message) {
        this.messages.unshift(message); // Add to top
        console.log(this.messages)
        this.changeDetection.detectChanges();
      }
      console.log("Message received:", userNotification);
    });
  }

  onNotificationClick(msg: string) {
  msg = msg.toLowerCase(); // normalize casing to avoid mismatch

  if (msg.includes('leave')) {
    this.router.navigate(['app/leaverequest']);
  } else if (msg.includes('role')) {
    if( msg.includes("employee")){
      this.router.navigate(['app/employee/management']);
    }
    else {
      this.router.navigate(['app/manager/management']);
    }
  } else if (msg.includes('approved as employee')) {
    this.router.navigate(['app/employee/profile']);
  } else if (msg.includes('approved as manager')) {
    this.router.navigate(['app/manager/dashboard']);
  }
}

}

import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SignalRAspNetCoreHelper } from '../../../../shared/helpers/SignalRAspNetCoreHelper';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-notification',
  templateUrl: './employee-notification.component.html',
  standalone: true,
  styleUrl: '../../../../style.css',
  imports: [CommonModule]
})
export class EmployeeNotificationComponent implements OnInit {

  messages: string[] = [];
  constructor(private changeDetection : ChangeDetectorRef){
    
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
}

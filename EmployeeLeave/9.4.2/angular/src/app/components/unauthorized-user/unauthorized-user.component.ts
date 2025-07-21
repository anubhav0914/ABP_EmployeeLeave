import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-unauthorized-user',
  standalone:true,
  templateUrl: './unauthorized-user.component.html',
  styleUrls: ['./unauthorized-user.component.scss']
})
export class UnauthorizedUserComponent {
  constructor(private location: Location) {}

  goBack(): void {
    this.location.back();
  }
}

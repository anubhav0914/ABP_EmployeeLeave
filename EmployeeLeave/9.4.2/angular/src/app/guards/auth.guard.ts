import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('access_token');
    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    const decoded: any = jwtDecode(token);
    const roles = decoded?.role ? (Array.isArray(decoded.role) ? decoded.role : [decoded.role]) : [];

    if (!roles.length) {
      this.router.navigate(['/create-account']);
      return false;
    }

    return true;
  }
}

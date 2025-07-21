import { Injectable } from '@angular/core';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '../session/app-session.service';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable()
export class AppRouteGuard {
    constructor(
        private _permissionChecker: PermissionCheckerService,
        private _router: Router,
        private _sessionService: AppSessionService,
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this._sessionService.user) {
            this._router.navigate(['/account/login']);
            return false;
        }

        const token = abp.auth.getToken();
        const decoded = jwtDecode(token);
        const userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        console.log("at auth role is ", userRole)

        // Check roles first if defined
        const allowedRoles: string[] = route.data['allowedRoles'];
        if (allowedRoles && allowedRoles.length > 0 && !allowedRoles.includes(userRole)) {
            this._router.navigate(['app/unauthorized']);
            return false;
        }

        // Then check permissions if defined
        if (!route.data || !route.data['permission']) {
            return true;
        }

        if (this._permissionChecker.isGranted(route.data['permission'])) {
            return true;
        }

        this._router.navigate([this.selectBestRoute()]);
        return false;
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    selectBestRoute(): string {
        if (!this._sessionService.user) {
            return '/account/login';
        }

        if (this._permissionChecker.isGranted('Pages.Users')) {
            return '/app/admin/users';
        }

        return '/app/home';
    }
}

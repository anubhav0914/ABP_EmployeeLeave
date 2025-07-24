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

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        // Wait for session to be initialized (in case login is in progress)
        if (!this._sessionService.user) {
            await this._sessionService.init(); // wait for session info to load
        }

        const token = abp.auth.getToken();
        const userRole = this.getUserRoleFromToken(token);

        if (!this._sessionService.user || !userRole) {
            this._router.navigate(['/account/login']);
            return false;
        }

        const allowedRoles: string[] = route.data['allowedRoles'];
        if (allowedRoles && allowedRoles.length > 0 && !allowedRoles.includes(userRole)) {
            this._router.navigate(['app/unauthorized']);
            return false;
        }

        if (!route.data || !route.data['permission']) {
            return true;
        }

        if (this._permissionChecker.isGranted(route.data['permission'])) {
            return true;
        }

        this._router.navigate([this.selectBestRoute()]);
        return false;
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        return this.canActivate(route, state);
    }

    private getUserRoleFromToken(token: string | null): string | null {
        if (!token || token.trim() === '' || !token.includes('.')) {
            return null;
        }

        try {
            const decoded: any = jwtDecode(token);
            return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
        } catch (error) {
            console.error('Invalid token:', error);
            return null;
        }
    }

    private selectBestRoute(): string {
        if (!this._sessionService.user) {
            return '/account/login';
        }

        if (this._permissionChecker.isGranted('Pages.Users')) {
            return '/app/admin/users';
        }

        return '/app/home';
    }
}

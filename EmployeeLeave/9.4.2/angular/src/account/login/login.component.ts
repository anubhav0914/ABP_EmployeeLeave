import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { TokenService } from 'abp-ng2-module'; // ✅ For reading accessToken
import {jwtDecode} from 'jwt-decode';

@Component({
  templateUrl: './login.component.html',
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase {
  submitting = false;

  constructor(
    injector: Injector,
    public authService: AppAuthService,
    private _sessionService: AbpSessionService,
    private _tokenService: TokenService, // ✅ Injected
    private _router: Router // ✅ Injected for redirection
  ) {
    super(injector);
  }

  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    return !!this._sessionService.tenantId;
  }

  login(): void {
  

    console.log("trying to login");
    this.submitting = true;

    this.authService.authenticate(() => {
      this.submitting = false;

      const token = abp.auth.getToken(); // ✅ Safely fetch the token
      if (!token) {
        console.error("No token found.");
        return;
      }

      const decoded: any = jwtDecode(token); // ✅ Decode the token
      const role = decoded?.role || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      switch (role) {
        case 'Employee':
          this._router.navigate(['app/dashboard/employee']);
          break;
        case 'Manager':
          this._router.navigate(['app/manager/dashboard']);
          break;
        case 'Founder':
          this._router.navigate(['app/founder/dashboard']);
          break;
        default:
          this._router.navigate(['app/dashboard/user']);
      }
    });
  }
}

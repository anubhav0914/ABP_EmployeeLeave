import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AppComponent } from './app.component';
import { UnauthorizedUserComponent } from "./components/unauthorized-user/unauthorized-user.component"
import { EmployeeDashboardComponent } from "./components/employee-dashboard/employee-dashboard.component"
import {EmployeeProfileComponent} from "./components/employee-profile/employee-profile.component"
import {RoleSelectionComponent} from "./components/role-selection/role-selection.component"
import {RegisterEmployeeComponent} from "./components/register-employee/register-employee.component"
import {LeaveApprovalComponent } from "./components/leaverequestlist/leave-approval.component"
import {EmployeeWithStatusComponent} from "./components/employee-with-status/employee-with-status.component"
import {ManagerManagementComponent} from "./dashboards/manager-management.component"
import {RegisterManagerComponent} from "./components/register-manager/register-manager.component"
import {LeaveApplyFormComponent} from "./components/leave-request/leave-apply-form.component"
import  {LeaveTypeServiceComponent} from "./components/leave-types-management/leave-type-service/leave-type-service.component"
import {DashboardFounderComponent} from "./components/founder/dashboard/dashboard.component"
import {ManagerDashboardComponent} from "./components/manager-dashboard/manager-dashboard.component"
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    {
                        path: '',
                        redirectTo: 'auth',
                        pathMatch: 'full'
                    },
                    {
                        path: 'employees',
                        loadComponent: () => import('./components/employee-list/employee-list.component').then(m => m.EmployeeListComponent),
                        // canActivate: [AppRouteGuard] /
                    },
                    {
                        path: 'auth',
                        loadChildren: () =>
                            import('./auth/auth.module').then((m) => m.AuthModule)
                    },
                    {
                        path: 'unauthorized',
                        component: UnauthorizedUserComponent
                    },
                    {
                        path: 'employee/profile',
                        component: EmployeeProfileComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Employee'] }

                    }
                    ,
                    {
                        path: 'home',
                        loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path:"leaveType/management",
                        component: LeaveTypeServiceComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Founder'] }
                    },
                    {
                        path:"founder/dashboard",
                        component: DashboardFounderComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Founder'] }
                    },
                    
                    {
                        path: 'about',
                        loadChildren: () => import('./about/about.module').then((m) => m.AboutModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'users',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        data: { permission: 'Pages.Users' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'roles',
                        loadChildren: () => import('./roles/roles.module').then((m) => m.RolesModule),
                        data: { permission: 'Pages.Roles' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'dashboard/employee',
                        component: EmployeeDashboardComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Employee'] }
                    },
                    {
                        path: 'leaverequest',
                        component: LeaveApprovalComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Founder','Manager'] }
                    },
                    {
                        path: 'employees/withStatus',
                        component: EmployeeWithStatusComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Founder'] }
                    },
                    {
                        path: 'manager/management',
                        component: ManagerManagementComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Founder'] }
                    },
                    {
                        path: 'manager/dashboard',
                        component: ManagerDashboardComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Manager'] }
                    },
                    {
                        path: 'manager/register',
                        component: RegisterManagerComponent,
                    },
                    {
                        path: 'dashboard/user',
                        component: RoleSelectionComponent
                    },
                    {
                        path: 'apply/leave',
                        component: LeaveApplyFormComponent,
                        canActivate: [AppRouteGuard],
                        data: { allowedRoles: ['Employee'] }
                    },
                    {
                        path: 'register/employee',
                        component: RegisterEmployeeComponent,
                    },

                    {
                        path: 'tenants',
                        loadChildren: () => import('./tenants/tenants.module').then((m) => m.TenantsModule),
                        data: { permission: 'Pages.Tenants' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'update-password',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        canActivate: [AppRouteGuard]
                    }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }

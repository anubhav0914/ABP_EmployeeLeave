using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace EmployeeLeave.Authorization
{
    public class EmployeeLeaveAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Employee_Create, L("Create Employee"));
            context.CreatePermission(PermissionNames.Employee_Update, L("Update Employee"));
            context.CreatePermission(PermissionNames.Employee_Delete, L("Delete Employee"));
            context.CreatePermission(PermissionNames.Employee_View, L("View Employees"));

            context.CreatePermission(PermissionNames.LeaveType_Create, L("Create LeaveType"));
            context.CreatePermission(PermissionNames.LeaveType_Update, L("Update LeaveType"));
            context.CreatePermission(PermissionNames.LeaveType_Delete, L("Delete LeaveType"));
            context.CreatePermission(PermissionNames.LeaveType_View, L("View LeaveTypes"));

            context.CreatePermission(PermissionNames.LeaveRequest_Create, L("Create LeaveRequest"));
            context.CreatePermission(PermissionNames.LeaveRequest_ApproveReject, L("Approve/Reject LeaveRequest"));
            context.CreatePermission(PermissionNames.LeaveRequest_View_All, L("View All LeaveRequests"));
            context.CreatePermission(PermissionNames.LeaveRequest_View_Own, L("View Own LeaveRequests"));
            context.CreatePermission(PermissionNames.LeaveRequest_Cancel_Own, L("Cancel Own LeaveRequests"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EmployeeLeaveConsts.LocalizationSourceName);
        }
    }
}

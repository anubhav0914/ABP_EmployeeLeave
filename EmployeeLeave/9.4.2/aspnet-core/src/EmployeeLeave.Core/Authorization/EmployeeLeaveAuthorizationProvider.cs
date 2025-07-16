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

            var pages = context.CreatePermission("Pages", L("Pages"));

            var employee = pages.CreateChildPermission("Pages.Employee", L("Employee"));
            employee.CreateChildPermission("Pages.Employee.Create", L("CreateEmployee"));
            employee.CreateChildPermission("Pages.Employee.Update", L("UpdateEmployee"));
            employee.CreateChildPermission("Pages.Employee.Delete", L("DeleteEmployee"));
            employee.CreateChildPermission("Pages.Employee.View", L("ViewEmployees"));

            var leaveType = pages.CreateChildPermission("Pages.LeaveType", L("LeaveType"));
            leaveType.CreateChildPermission("Pages.LeaveType.Create", L("CreateLeaveType"));
            leaveType.CreateChildPermission("Pages.LeaveType.Update", L("UpdateLeaveType"));
            leaveType.CreateChildPermission("Pages.LeaveType.Delete", L("DeleteLeaveType"));
            leaveType.CreateChildPermission("Pages.LeaveType.View", L("ViewLeaveTypes"));

            var leaveRequest = pages.CreateChildPermission("Pages.LeaveRequest", L("LeaveRequest"));
            leaveRequest.CreateChildPermission("Pages.LeaveRequest.Create", L("CreateLeaveRequest"));
            leaveRequest.CreateChildPermission("Pages.LeaveRequest.View", L("ViewLeaveRequests"));
            leaveRequest.CreateChildPermission("Pages.LeaveRequest.ApproveReject", L("ApproveRejectLeave"));
            leaveRequest.CreateChildPermission("Pages.LeaveRequest.CancelDelete", L("CancelDeleteLeaveRequest"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EmployeeLeaveConsts.LocalizationSourceName);
        }
    }
}

using Abp.Authorization;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;

namespace EmployeeLeave.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

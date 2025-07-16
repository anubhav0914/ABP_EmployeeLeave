using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeave.Controllers
{
    public abstract class EmployeeLeaveControllerBase: AbpController
    {
        protected EmployeeLeaveControllerBase()
        {
            LocalizationSourceName = EmployeeLeaveConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeLeave.Configuration;

namespace EmployeeLeave.Web.Host.Startup
{
    [DependsOn(
       typeof(EmployeeLeaveWebCoreModule))]
    public class EmployeeLeaveWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EmployeeLeaveWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(typeof(EmployeeLeaveWebHostModule).GetAssembly());
            // IocManager.Register<RoleSeeder>();

        }
    }
}

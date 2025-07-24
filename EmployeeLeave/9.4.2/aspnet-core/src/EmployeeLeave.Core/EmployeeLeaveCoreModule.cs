using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Configuration;
using EmployeeLeave.Localization;
using EmployeeLeave.MultiTenancy;
using EmployeeLeave.Timing;

namespace EmployeeLeave
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class EmployeeLeaveCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            EmployeeLeaveLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = EmployeeLeaveConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));

            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = EmployeeLeaveConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = EmployeeLeaveConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeLeaveCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}

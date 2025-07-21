using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using EmployeeLeave.EntityFrameworkCore.Seed.Host;
using EmployeeLeave.EntityFrameworkCore.Seed.Tenants;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EmployeeLeave.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Model;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Authorization;
using EmployeeLeaveManagementSystem.EntityFrameworkCore.Seed;
using Abp.Runtime.Session;
using EmployeeLeave.MultiTenancy;

namespace EmployeeLeave.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<EmployeeLeaveDbContext>(iocResolver, context =>
            {
                context.SuppressAutoSetTenantId = true;
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
                new InitialHostDbBuilder(
                    context
                ).Create();

                var roleManager = iocResolver.Resolve<RoleManager>();
                var userManager = iocResolver.Resolve<UserManager>();
                var founderRepository = iocResolver.Resolve<IRepository<Founder, long>>();
                var unitOfWorkManager = iocResolver.Resolve<IUnitOfWorkManager>();
                var localizationManager = iocResolver.Resolve<ILocalizationManager>();
                var permissionManager = iocResolver.Resolve<IPermissionManager>();
                var tenatmanger = iocResolver.Resolve<TenantManager>();

                new DefaultFounderCreator(userManager, roleManager, founderRepository, unitOfWorkManager,tenatmanger)
                    .CreateAsync().GetAwaiter().GetResult();
            new RolesDataSeeder(
                        roleManager,
                        userManager,
                        unitOfWorkManager,
                        permissionManager,
                        tenatmanger
                            ).SeedAsync().GetAwaiter().GetResult();

                context.SaveChanges();


            });
        }

        
        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }

}

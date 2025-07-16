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

namespace EmployeeLeave.EntityFrameworkCore.Seed
{
    public static class SeedHelper
{
    public static void SeedHostDb(IIocResolver iocResolver)
    {
        WithDbContext<EmployeeLeaveDbContext>(iocResolver, context => SeedHostDbInternal(iocResolver, context));
    }

    private static void SeedHostDbInternal(IIocResolver iocResolver, EmployeeLeaveDbContext context)
    {
        context.SuppressAutoSetTenantId = true;

        var roleManager = iocResolver.Resolve<RoleManager<Role>>();
        var userManager = iocResolver.Resolve<UserManager<User>>();
        var founderRepository = iocResolver.Resolve<IRepository<Founder, long>>();
        var unitOfWorkManager = iocResolver.Resolve<IUnitOfWorkManager>();

        new InitialHostDbBuilder(context, roleManager, userManager, unitOfWorkManager, founderRepository).Create();

        new DefaultFounderCreator(userManager, roleManager, founderRepository, unitOfWorkManager)
            .CreateAsync().GetAwaiter().GetResult();

        new DefaultTenantBuilder(context).Create();
        new TenantRoleAndUserBuilder(context, 1).Create();
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

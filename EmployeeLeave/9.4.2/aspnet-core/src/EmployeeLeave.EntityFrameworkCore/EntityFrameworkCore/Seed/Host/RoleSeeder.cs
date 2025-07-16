using System.Threading.Tasks;
using Abp.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using Abp.Domain.Uow;

namespace EmployeeLeave.EntityFrameworkCore.Seed.Host
{
    public class RoleSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;


        public RoleSeeder(RoleManager<Role> roleManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            
        }

        public async Task SeedAsync()
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                await CreateRoleIfNotExists("Employee");
                await CreateRoleIfNotExists("Manager");
                await CreateRoleIfNotExists("Founder");
                await uow.CompleteAsync();
            }
        }

        private async Task CreateRoleIfNotExists(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new Role(null, roleName, roleName);
                var result = await _roleManager.CreateAsync(role);
            
                if (!result.Succeeded)
                {
                    throw new System.Exception("Role creation failed: " + string.Join(", ", result.Errors));
                }
            }
        }
    }
}

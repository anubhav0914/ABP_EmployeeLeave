using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Model;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeave.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly EmployeeLeaveDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Founder, long> _founderRepository;


        public InitialHostDbBuilder(EmployeeLeaveDbContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager,
        IUnitOfWorkManager unitOfWorkManager,
        IRepository<Founder, long> founderRepository
    )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _founderRepository = founderRepository;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            // CreateRoles();
            // _context.SaveChanges();
            new RoleSeeder(_roleManager, _unitOfWorkManager).SeedAsync().GetAwaiter().GetResult();
            _context.SaveChanges();
        }
        // private void CreateRoles()
        // {
        //     new RoleSeeder(_roleManager).SeedAsync().GetAwaiter().GetResult();
        // }


    }
}

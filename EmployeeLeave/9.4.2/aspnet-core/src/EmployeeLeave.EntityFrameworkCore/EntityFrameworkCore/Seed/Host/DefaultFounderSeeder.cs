using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

public class DefaultFounderCreator
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IRepository<Founder, long> _founderRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DefaultFounderCreator(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IRepository<Founder, long> founderRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _founderRepository = founderRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task CreateAsync()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            const string founderUserName = "founder1";
            const string founderEmail = "anubhav@gmail.com";
            const string founderPassword = "Anubhav@1805";

            var user = _userManager.Users
                .FirstOrDefault(u => u.UserName == founderUserName);

            if (user == null)
            {
                user = new User
                {
                    UserName = founderUserName,
                    Name = "Anubhav",
                    Surname = "Gupta",
                    EmailAddress = founderEmail,
                    IsEmailConfirmed = true,
                };
                user.SetNormalizedNames();

                var result = await _userManager.CreateAsync(user, founderPassword);
                result.CheckErrors();
            }

            int tenantId = user.TenantId ?? 3;

            var founderRole = await _roleManager.FindByNameAsync("Founder");
            if (founderRole == null)
            {
                founderRole = new Role(tenantId, "Founder", "Founder")
                {
                    IsStatic = true,
                    IsDefault = false
                };
                await _roleManager.CreateAsync(founderRole);
            }

            if (!await _userManager.IsInRoleAsync(user, "Founder"))
            {
                await _userManager.AddToRoleAsync(user, "Founder");
            }

            var founderExists = _founderRepository.GetAll()
                .Any(f => f.UserId == user.Id);

            if (!founderExists)
            {
                var founder = new Founder
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    TenantId = tenantId
                };

                await _founderRepository.InsertAsync(founder);
            }

            await uow.CompleteAsync();
        }
    }
}

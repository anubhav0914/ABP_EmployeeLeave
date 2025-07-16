using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Authorization.Accounts.Dto;

namespace EmployeeLeave.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

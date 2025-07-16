using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Sessions.Dto;

namespace EmployeeLeave.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

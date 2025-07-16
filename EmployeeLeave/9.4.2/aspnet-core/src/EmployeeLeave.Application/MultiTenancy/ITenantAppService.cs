using Abp.Application.Services;
using EmployeeLeave.MultiTenancy.Dto;

namespace EmployeeLeave.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


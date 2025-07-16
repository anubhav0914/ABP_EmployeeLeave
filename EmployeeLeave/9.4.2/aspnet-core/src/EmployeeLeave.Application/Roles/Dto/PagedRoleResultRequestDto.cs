using Abp.Application.Services.Dto;

namespace EmployeeLeave.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}


using System.Threading.Tasks;
using EmployeeLeave.Configuration.Dto;

namespace EmployeeLeave.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

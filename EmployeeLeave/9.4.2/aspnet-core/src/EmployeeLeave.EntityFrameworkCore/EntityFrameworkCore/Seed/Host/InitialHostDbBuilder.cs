
namespace EmployeeLeave.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly EmployeeLeaveDbContext _context;

        public InitialHostDbBuilder(EmployeeLeaveDbContext context)
        {
            _context = context;

        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            
        }
        


    }
}

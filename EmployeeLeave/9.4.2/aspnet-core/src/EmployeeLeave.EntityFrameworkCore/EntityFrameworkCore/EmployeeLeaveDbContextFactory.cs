using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EmployeeLeave.Configuration;
using EmployeeLeave.Web;

namespace EmployeeLeave.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EmployeeLeaveDbContextFactory : IDesignTimeDbContextFactory<EmployeeLeaveDbContext>
    {
        public EmployeeLeaveDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EmployeeLeaveDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EmployeeLeaveDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EmployeeLeaveConsts.ConnectionStringName));

            return new EmployeeLeaveDbContext(builder.Options);
        }
    }
}

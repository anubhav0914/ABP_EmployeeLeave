using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeave.EntityFrameworkCore
{
    public static class EmployeeLeaveDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EmployeeLeaveDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EmployeeLeaveDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

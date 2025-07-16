using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.MultiTenancy;
using EmployeeLeave.Model;

namespace EmployeeLeave.EntityFrameworkCore
{
    public class EmployeeLeaveDbContext : AbpZeroDbContext<Tenant, Role, User, EmployeeLeaveDbContext>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Founder> Founder { get; set; }


        public EmployeeLeaveDbContext(DbContextOptions<EmployeeLeaveDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveRequestConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
            modelBuilder.ApplyConfiguration(new FounderConfiguration());
        
        }
    }
}

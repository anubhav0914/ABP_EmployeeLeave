using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using EmployeeLeave;

namespace EmployeeLeave
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(e => e.Department)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(e => e.DateOfJoining)
                .IsRequired(true);
            
             builder.HasIndex(e => e.UserId).IsUnique(true);

    }
    }
}

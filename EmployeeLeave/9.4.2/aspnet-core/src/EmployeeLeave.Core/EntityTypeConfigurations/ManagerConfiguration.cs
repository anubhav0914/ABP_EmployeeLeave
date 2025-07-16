using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeLeave.Model;

namespace EmployeeLeave
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("Managers");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.DateOfJoining)
                   .IsRequired();

            builder.Property(m => m.Work_experince_year)
                   .IsRequired(true);

            builder.Property(m => m.IsActive)
                   .IsRequired(true); // until approved it ca nbe null 

            builder.Property(m => m.IsApproved_by_Founder) 
                   .IsRequired(true);

            builder.HasOne(m => m.User)
                   .WithMany() // assuming no navigation in IdentityUser
                   .HasForeignKey(m => m.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

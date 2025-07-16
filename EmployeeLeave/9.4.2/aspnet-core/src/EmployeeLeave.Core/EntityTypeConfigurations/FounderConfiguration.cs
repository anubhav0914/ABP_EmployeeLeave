using EmployeeLeave.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeLeave
{
    public class FounderConfiguration : IEntityTypeConfiguration<Founder>
    {
        public void Configure(EntityTypeBuilder<Founder> builder)
        {
            builder.ToTable("Founders");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserName)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasOne(f => f.User)
                   .WithMany() // assuming IdentityUser has no navigation back
                   .HasForeignKey(f => f.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.LeaveRequests)
                   .WithOne(lr => lr.Founder)
                   .HasForeignKey(lr => lr.Founder_Id_approved_rejected_BY)
                   .OnDelete(DeleteBehavior.Restrict); // Or Cascade, based on your logic
        }
    }
}

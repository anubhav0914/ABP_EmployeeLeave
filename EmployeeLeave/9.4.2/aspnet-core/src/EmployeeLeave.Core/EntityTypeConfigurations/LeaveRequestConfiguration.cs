using System;
using EmployeeLeave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeLeave;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.ToTable("LeaveRequests");

        builder.HasKey(lr => lr.Id);

        builder.HasOne(lr => lr.Employee)
               .WithMany()
               .HasForeignKey(lr => lr.EmployeeId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(lr => lr.Founder)
               .WithMany()
               .HasForeignKey(lr => lr.Founder_Id_approved_rejected_BY)
               .IsRequired(false) 
               .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(lr => lr.LeaveType)
               .WithMany()
               .HasForeignKey(lr => lr.LeaveTypeId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(lr => lr.FromDate)
               .IsRequired();

        builder.Property(lr => lr.ToDate)
               .IsRequired();

        builder.Property(lr => lr.Status)
               .IsRequired();
        // optional 
        builder.Property(lr => lr.Reason)
               .HasMaxLength(500); 
    }

}

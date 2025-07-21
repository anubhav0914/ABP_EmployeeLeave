using System;
using Abp.Domain.Entities;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Model;

namespace EmployeeLeave;

public enum LeaveStatus
{
    Pending,
    Approved,
    Rejected
}
public class LeaveRequest : Entity<long>
{

    public long EmployeeId { get; set; }
    // naivgation
    public Employee Employee { get; set; }

    public long LeaveTypeId { get; set; }
    // navigation
    public LeaveType LeaveType { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public long? Founder_Id_approved_rejected_BY { get; set; }
    public User? User { get; set; }

    public string? Reason { get; set; }

    public LeaveStatus Status { get; set; }
}

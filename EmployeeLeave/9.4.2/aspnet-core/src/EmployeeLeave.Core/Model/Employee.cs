using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using EmployeeLeave.Authorization.Users;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeave;

public class Employee : Entity<long>
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Department { get; set; }
    public long UserId { get; set; }

    public bool IsAppliedForEmploee { get; set; } = false;
    public bool IsApprovedByFounder { get; set; } = false;


    public virtual User User { get; set; }
    public int Work_experince_year { get; set; }
    public DateOnly DateOfJoining { get; set; }
    public ICollection<LeaveRequest> LeaveRequests { get; set; }
}


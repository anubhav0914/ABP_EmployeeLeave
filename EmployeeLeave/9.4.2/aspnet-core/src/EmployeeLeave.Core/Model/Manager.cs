using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using EmployeeLeave.Authorization.Users;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeave.Model;

public class Manager : Entity<long>

{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime DateOfJoining { get; set; }
    public int Work_experince_year { get; set; }

    public bool IsActive { get; set; } = false;
    public bool IsAppliedForEmployeeForManager { get; set; } = false;

    public bool IsApproved_by_Founder { get; set; } = false;


}

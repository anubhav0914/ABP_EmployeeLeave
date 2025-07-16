using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using EmployeeLeave.Authorization.Users;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeave.Model;

public class Founder : Entity<long>
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public string UserName { get; set; }
    public ICollection<LeaveRequest> LeaveRequests { get; set; }
    public int? TenantId { get; set; }
}

using System;
using System.Collections.Generic;
using Abp.Domain.Entities;

namespace EmployeeLeave;

public class LeaveType : Entity<long>,IMustHaveTenant
{    
    public string Name { get; set; }
    public string Description { get; set; }
    public int TenantId { get; set; }


    public int MaxDaysAllowed { get; set; }
    public ICollection<LeaveRequest> LeaveRequests { get; set; }

}

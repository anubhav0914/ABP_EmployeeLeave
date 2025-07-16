using System;
using Abp.AutoMapper;

namespace EmployeeLeave.Domain.DTOs;


[AutoMapFrom(typeof(LeaveType))]
[AutoMapTo(typeof(LeaveType))]
public class LeaveTypeDto
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MaxDaysAllowed { get; set; }

}

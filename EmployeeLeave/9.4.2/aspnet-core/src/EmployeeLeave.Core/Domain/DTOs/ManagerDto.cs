using System;
using Abp.AutoMapper;
using EmployeeLeave.Model;

namespace EmployeeLeave.Domain.DTOs;

[AutoMapFrom(typeof(Manager))]
[AutoMapTo(typeof(Manager))]
public class ManagerDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime DateOfJoining { get; set; }
    public int Work_experince_year { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsApproved_by_Founder { get; set; } = false;

}

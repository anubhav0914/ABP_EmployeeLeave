using System;
using Abp.AutoMapper;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Model;
using EmployeeLeave.Users.Dto;

namespace EmployeeLeave.ResponseDTOs;

[AutoMapFrom(typeof(Manager))]
[AutoMapTo(typeof(Manager))]
public class ManagerResponseDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime DateOfJoining { get; set; }
    public int Work_experince_year { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsApproved_by_Founder { get; set; } = false;

    public UserDto User { get; set; }

}
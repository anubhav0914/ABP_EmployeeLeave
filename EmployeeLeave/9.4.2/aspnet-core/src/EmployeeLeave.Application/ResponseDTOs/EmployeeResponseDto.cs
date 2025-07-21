using System;
using Abp.AutoMapper;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Users.Dto;

namespace EmployeeLeave.ResponseDTOs;

[AutoMapFrom(typeof(Employee))]
[AutoMapTo(typeof(Employee))]
public class EmployeeResponseDto
{


    public long Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Department { get; set; }
    public bool IsAppliedForEmployeeForManager { get; set; } 
    public bool IsApprovedByFounder { get; set; } = false;

    public DateOnly DateOfJoining { get; set; }
    
    public UserDto user { get; set; }


}

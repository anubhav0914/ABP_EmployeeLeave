using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace EmployeeLeave.Domain.DTOs;


[AutoMapFrom(typeof(Employee))]
[AutoMapTo(typeof(Employee))]
public class EmployeeDto
{   
    

    public long Id { get; set; }
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Department { get; set; }

    [Required]
    public DateOnly DateOfJoining { get; set; }


}


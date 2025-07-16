using System;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;

namespace EmployeeLeave;

public class LeaveTypeAutomaper :Profile
{
    public LeaveTypeAutomaper()
    {
        CreateMap<LeaveTypeDto, LeaveType>();

        CreateMap<LeaveType, LeaveTypeDto>();

    }
  
}

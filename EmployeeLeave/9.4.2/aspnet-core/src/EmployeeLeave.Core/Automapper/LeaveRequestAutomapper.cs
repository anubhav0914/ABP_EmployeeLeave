using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;

namespace EmployeeLeave;

public class LeaveRequestAutomapper : Profile
{       
    public LeaveRequestAutomapper()
    {
        CreateMap<LeaveRequestCreateUpdateDto, LeaveRequest>();

        CreateMap<LeaveRequest, LeaveRequestCreateUpdateDto>();

    }
      
}

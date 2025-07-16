using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Model;

namespace EmployeeLeave;

public class ManagerAutomapper : Profile
{       
    public ManagerAutomapper()
    {
        CreateMap<ManagerDto, Manager>();

        CreateMap<Manager, ManagerDto>();

    }
      
}

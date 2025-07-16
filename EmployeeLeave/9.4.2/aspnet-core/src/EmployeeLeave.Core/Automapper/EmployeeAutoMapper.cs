using System;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave;

namespace EmployeeLeave;

public class EmployeeAutoMapper : Profile
{
    public EmployeeAutoMapper()
    {
        CreateMap<EmployeeDto, Employee>()
    .ForMember(dest => dest.DateOfJoining,
        opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfJoining.ToDateTime(TimeOnly.MinValue))));

        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DateOfJoining,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfJoining.ToDateTime(TimeOnly.MinValue))));

    }
}

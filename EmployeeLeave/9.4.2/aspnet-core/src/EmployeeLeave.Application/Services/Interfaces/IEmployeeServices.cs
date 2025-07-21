using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.ResponseDTOs;
using EmployeeLeave.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeave.Services.Interfaces;

public interface IEmployeeServices : IApplicationService
{
    public Task<ApiResponse<EmployeeResponseDto>> RegisterEmployee(EmployeeDto dto);
    public Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployee();
    public Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployeeRequested();
    public Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployeeApproved();

    public Task<ApiResponse<EmployeeResponseDto>> GetById(long id);
    public Task<ApiResponse<EmployeeResponseDto>> DeleteEmployee(long id);
    public Task<ApiResponse<EmployeeResponseDto>> Update(EmployeeDto dto);
    public Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByUserId(long userId);


}

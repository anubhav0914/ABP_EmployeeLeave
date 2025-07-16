using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Utils;

namespace EmployeeLeave.Services.Interfaces;

public interface ILeaveTypeServices : IApplicationService
{
      public Task<ApiResponse<LeaveTypeDto>> AddLeaveType(LeaveTypeDto dto);
    public Task<ApiResponse<LeaveTypeDto>> GetById(long id);
    public Task<ApiResponse<LeaveTypeDto>> Update(LeaveTypeDto dto);
    public Task<ApiResponse<List<LeaveTypeDto>>> GetAll();
    public Task<ApiResponse<LeaveTypeDto>> Delete(long id);

}

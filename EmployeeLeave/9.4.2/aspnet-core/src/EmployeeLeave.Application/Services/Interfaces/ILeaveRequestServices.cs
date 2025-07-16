using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Utils;

namespace EmployeeLeave.Services.Interfaces;

public interface ILeaveRequestServices: IApplicationService
{

     public Task<ApiResponse<LeaveRequestCreateUpdateDto>> AddLeaveRequest(LeaveRequestCreateUpdateDto dto);
    public Task<ApiResponse<LeaveRequestCreateUpdateDto>> GetById(long id);
    public Task<ApiResponse<LeaveRequestCreateUpdateDto>> Update(LeaveRequestCreateUpdateDto dto);
    public Task<ApiResponse<List<LeaveRequestCreateUpdateDto>>> GetAll();
    public Task<ApiResponse<LeaveRequestCreateUpdateDto>> Delete(long id);
    public Task<ApiResponse<List<LeaveRequestCreateUpdateDto>>> GetByStatus(LeaveStatus statusId);
}

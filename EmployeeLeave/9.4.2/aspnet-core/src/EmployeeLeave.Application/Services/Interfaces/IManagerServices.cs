using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.ResponseDTOs;
using EmployeeLeave.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeave.Services.Interfaces;

public interface IManagerServices : IApplicationService
{
    public Task<IActionResult> ApporveLeave(ApproveLeaveDto dto);
    public Task<IActionResult> RegisterManger(ManagerDto dto);

    public Task<ApiResponse<List<ManagerResponseDto>>> GetAllManager();
    public Task<ApiResponse<ManagerResponseDto>> GetById(long id);
    public Task<ApiResponse<ManagerResponseDto>> DeleteManager(long id);
    public Task<ApiResponse<ManagerResponseDto>> UpdateManager(ManagerDto dto);

}

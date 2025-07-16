using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.ResponseDTOs;
using EmployeeLeave.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeave.Services.Interfaces;

public interface IFounderServices : IApplicationService
{
    public Task<IActionResult> ApproveLeave(ApproveLeaveDto employeeId);
    public Task<IActionResult> Delete(long id);


}

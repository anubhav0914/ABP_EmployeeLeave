using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Model;
using EmployeeLeave.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeave.Services.EntityServices;

public class FounderServices : ApplicationService, IFounderServices
{   
    private readonly IRepository<Employee, long> _employeeRepository;

    private readonly IRepository<LeaveRequest, long> _leaveRepository;
    private readonly IRepository<Founder, long> _founderRepository;

   

    public FounderServices(IRepository<Employee, long> employeeRepository,
    IRepository<Manager, long> managerRepository,
    IRepository<LeaveRequest, long> leaveRepository,
    IRepository<Founder, long> founderREpository
    )
    {
        _employeeRepository = employeeRepository;
        _leaveRepository = leaveRepository;
        _founderRepository = founderREpository;

    }
    public async Task<IActionResult> ApproveLeave(ApproveLeaveDto dto)
    {
        try
        {
            var exisitingEmployee = await _employeeRepository.FirstOrDefaultAsync(dto.EmployeeId);
            var leaveRequest = await _leaveRepository.FirstOrDefaultAsync(dto.RequestId);

            if (exisitingEmployee == null || leaveRequest == null)
            {
                return new BadRequestObjectResult("No leave Reqest found Request not found.");
            }

            leaveRequest.Status = dto.Status;
            leaveRequest.Founder_Id_approved_rejected_BY = dto.Approved_by_ID;
            await _leaveRepository.UpdateAsync(leaveRequest);
            return new OkObjectResult("Leave approved successfully.");

        }
        catch (Exception err)
        {
            return new BadRequestObjectResult("Inernal Server Error" + err.Message);
        }
    }

    public async Task<IActionResult> Delete(long id)
    {
        await _founderRepository.DeleteAsync(id);
        return new OkObjectResult("Deleted  successfully.");
    }
}

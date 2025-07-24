using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using AutoMapper;
using EmployeeLeave.Authorization.Roles;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Model;
using EmployeeLeave.ResponseDTOs;
using EmployeeLeave.Services.Interfaces;
using EmployeeLeave.Users.Dto;
using EmployeeLeave.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EmployeeLeave.Services.EntityServices;

public class ManagerServices : ApplicationService, IManagerServices
{
    private readonly IRepository<Employee, long> _employeeRepository;
    private readonly IRepository<Manager, long> _managerRepository;

    private readonly IRepository<LeaveRequest, long> _leaveRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
        private readonly RoleManager _roleManager;
         private readonly IRepository<Founder, long> _founderRepository;
    private readonly IAbpSession _abpSession;
    private readonly INotificationPublisher _notificationPublisher;


    public ManagerServices(IRepository<Employee, long> employeeRepository,
     IRepository<LeaveRequest, long> leaveRepository,
      UserManager<User> userManager,
      IMapper mapper,
      IRepository<Manager, long> managerReposotory,
      IRepository<Founder, long> founderREpository,
            INotificationPublisher notificationPublisher,
            IAbpSession abpSession,
      RoleManager roleManager)
    {
        _employeeRepository = employeeRepository;
        _leaveRepository = leaveRepository;
        _userManager = userManager;
        _mapper = mapper;
        _managerRepository = managerReposotory;
        _roleManager = roleManager;
        _founderRepository = founderREpository;
        _abpSession = abpSession;
        _notificationPublisher = notificationPublisher;
    }
    public async Task<IActionResult> ApporveLeave(ApproveLeaveDto dto)
    {
        try
        {
            var exisitingEmployee = await _employeeRepository.FirstOrDefaultAsync(dto.EmployeeId);
            var leaveRequest = await _leaveRepository.FirstOrDefaultAsync(dto.RequestId);

            if (exisitingEmployee == null || leaveRequest == null)
            {
                return new BadRequestObjectResult("Employee or Leave Request not found.");
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

    public async Task<ApiResponse<ManagerResponseDto>> DeleteManager(long id)
    { 
       try  {
            var manager = await _managerRepository.FirstOrDefaultAsync(id);
            if (manager == null)
            {
                return new ApiResponse<ManagerResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Manager not found with the given ID.",
                    data = null
                };
            }

            await _managerRepository.DeleteAsync(manager);

            return new ApiResponse<ManagerResponseDto>
            {   
                status = true,
                statusCode = 200,
                message = "Manager deleted successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ManagerResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error deleting manager: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<List<ManagerResponseDto>>> GetAllManager()
    {
        try
        {
            var managers = await _managerRepository
                .GetAllIncluding(m => m.User)
                .ToListAsync();
            

            if (managers == null || managers.Count == 0)
            {
                return new ApiResponse<List<ManagerResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No Manager found.",
                    data = null
                };
            }

            var managerDtos = _mapper.Map<List<ManagerResponseDto>>(managers);
            for (int i = 0; i < managers.Count; i++)
            {
                managerDtos[i].Id = managers[i].Id;
                var userDto = new UserDto
                {
                    UserName = managers[i].User.UserName,
                    Name = managers[i].User.Name,
                    Surname = managers[i].User.Surname,
                    EmailAddress = managers[i].User.EmailAddress,
                    IsActive = managers[i].User.IsActive,
                    FullName = managers[i].User.FullName
                };
                managerDtos[i].User = userDto;
            }

            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Mnager retrieved successfully.",
                data = managerDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<ManagerResponseDto>> GetById(long id)
    {
        try
        {
            var manager = await _managerRepository
                .GetAllIncluding(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (manager == null)
            {
                return new ApiResponse<ManagerResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Mnager not found.",
                    data = null
                };
            }
            var userDto = new UserDto
                {
                    UserName = manager.User.UserName,
                    Name = manager.User.Name,
                    Surname = manager.User.Surname,
                    EmailAddress = manager.User.EmailAddress,
                    IsActive = manager.User.IsActive,
                    FullName = manager.User.FullName
                };

            var managerDto = _mapper.Map<ManagerResponseDto>(manager);
            managerDto.User = userDto;
            return new ApiResponse<ManagerResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Manager retrieved successfully.",
                data = managerDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ManagerResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employee: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<List<ManagerResponseDto>>> GetMangerRoleApproved()
    {
        try
        {
            var managers = await _managerRepository
                .GetAllIncluding(m => m.User)
                .Where(m=> m.IsApproved_by_Founder == true)
                .ToListAsync();
            

            if (managers == null || managers.Count == 0)
            {
                return new ApiResponse<List<ManagerResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No Manager found.",
                    data = null
                };
            }

            var managerDtos = _mapper.Map<List<ManagerResponseDto>>(managers);
            for (int i = 0; i < managers.Count; i++)
            {
                managerDtos[i].Id = managers[i].Id;
                var userDto = new UserDto
                {
                    UserName = managers[i].User.UserName,
                    Name = managers[i].User.Name,
                    Surname = managers[i].User.Surname,
                    EmailAddress = managers[i].User.EmailAddress,
                    IsActive = managers[i].User.IsActive,
                    FullName = managers[i].User.FullName
                };
                managerDtos[i].User = userDto;
            }

            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Mnager retrieved successfully.",
                data = managerDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<List<ManagerResponseDto>>> GetMangerRoleRequests()
    {
        try
        {
            var managers = await _managerRepository
                .GetAllIncluding(m => m.User)
                .Where(m=>m.IsApproved_by_Founder == false )
                .ToListAsync();

            if (managers == null || managers.Count == 0)
            {
                return new ApiResponse<List<ManagerResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No Manager found.",
                    data = null
                };
            }

            var managerDtos = _mapper.Map<List<ManagerResponseDto>>(managers);
            for (int i = 0; i < managers.Count; i++)
            {
                managerDtos[i].Id = managers[i].Id;
                var userDto = new UserDto
                {
                    UserName = managers[i].User.UserName,
                    Name = managers[i].User.Name,
                    Surname = managers[i].User.Surname,
                    EmailAddress = managers[i].User.EmailAddress,
                    IsActive = managers[i].User.IsActive,
                    FullName = managers[i].User.FullName
                };
                managerDtos[i].User = userDto;
            }

            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Mnager retrieved successfully.",
                data = managerDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ManagerResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<IActionResult> RegisterManger(ManagerDto dto)
    {
        var existUser = await _userManager.FindByIdAsync(dto.UserId.ToString());
         var tenantId = AbpSession.TenantId;

      var founder =  await _founderRepository.FirstOrDefaultAsync(f => f.TenantId == tenantId &&  f.UserName == "theFounder");

        if (existUser == null)
        {
            return new BadRequestObjectResult("there is now user with this id");
        }
        await _userManager.AddToRoleAsync(existUser, "Manager");
        var manager = _mapper.Map<Manager>(dto);
        manager.IsAppliedForEmployeeForManager = true;
        manager.IsApproved_by_Founder = false;
        
        var result = await _managerRepository.InsertAsync(manager);
            var userIdentifier = new UserIdentifier(_abpSession.TenantId, founder.UserId);
             await _notificationPublisher.PublishAsync(
              "LeaveApprovedNotification",
              new MessageNotificationData($"An user is Applied for a role of Manager with Id as  {manager.Id}! "),
              userIds: new[] { userIdentifier }
             );

        if (result != null)
        {
            return new OkObjectResult("The manager is add succesfully");
        }

        return new BadRequestObjectResult("someting went wrong while add the manager");

    }

    public async Task<ApiResponse<ManagerResponseDto>> UpdateManager(ManagerDto dto)
    {
        try
        {
            var user = await _managerRepository.FirstOrDefaultAsync(dto.Id);
            if (user == null)
            {
                return new ApiResponse<ManagerResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "User not found for the given email.",
                    data = null
                };
            }

            var manager = await _managerRepository.FirstOrDefaultAsync(e => e.UserId == user.Id);
            if (manager == null)
            {
                return new ApiResponse<ManagerResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Mnager not found for the given user.",
                    data = null
                };
            }

            _mapper.Map(dto, manager); // Update fields
            await _managerRepository.UpdateAsync(manager);

            var updatedDto = _mapper.Map<ManagerResponseDto>(manager);
            updatedDto.Id = manager.Id;

            return new ApiResponse<ManagerResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee updated successfully.",
                data = updatedDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ManagerResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error updating employee: {ex.Message}",
                data = null
            };
        }

        
    }
}

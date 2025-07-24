using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using EmployeeLeave.Authorization.Users;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Model;
using EmployeeLeave.ResponseDTOs;
using EmployeeLeave.Services.Interfaces;
using EmployeeLeave.Sessions.Dto;
using EmployeeLeave.Users.Dto;
using EmployeeLeave.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EmployeeLeave.Services.EntityServices;

public class EmployeeServices : ApplicationService, IEmployeeServices
{
    private readonly IRepository<Employee, long> _employeeRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _objectMapper;
    private readonly IRepository<Founder, long> _founderRepository;
    private readonly IAbpSession _abpSession;
    private readonly INotificationPublisher _notificationPublisher;



    public EmployeeServices(UserManager<User> userManager,
        IRepository<Employee, long> employeeRepository, IMapper objectMapper,
        IRepository<Founder, long> founderREpository,
            INotificationPublisher notificationPublisher,
            IAbpSession abpSession
        )
    {
        _userManager = userManager;
        _employeeRepository = employeeRepository;
        _objectMapper = objectMapper;
        _founderRepository = founderREpository;
        _abpSession = abpSession;
        _notificationPublisher = notificationPublisher;
    }
    public async Task<ApiResponse<EmployeeResponseDto>> DeleteEmployee(long id)
    {
        try
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(id);
            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Employee not found with the given ID.",
                    data = null
                };
            }

            await _employeeRepository.DeleteAsync(employee);

            return new ApiResponse<EmployeeResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee deleted successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<EmployeeResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error deleting employee: {ex.Message}",
                data = null
            };
        }
    }

    [AbpAuthorize("Employee.View")]
    public async Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployee()
    {
        try
        {
            var employees = await _employeeRepository.GetAllIncluding(m => m.User)
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return new ApiResponse<List<EmployeeResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No employees found.",
                    data = null
                };
            }

            var employeeDtos = _objectMapper.Map<List<EmployeeResponseDto>>(employees);
            for (int i = 0; i < employees.Count; i++)
            {
                employeeDtos[i].Id = employees[i].Id;
                var userDto = new UserDto
                {
                    UserName = employees[i].User.UserName,
                    Name = employees[i].User.Name,
                    Surname = employees[i].User.Surname,
                    EmailAddress = employees[i].User.EmailAddress,
                    IsActive = employees[i].User.IsActive,
                    FullName = employees[i].User.FullName
                };
                employeeDtos[i].user = userDto;
            }

            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Employees retrieved successfully.",
                data = employeeDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployeeApproved()
    {
        try
        {
            var employees = await _employeeRepository.GetAllIncluding(m => m.User)
            .Where(m=>m.IsApprovedByFounder == true)
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return new ApiResponse<List<EmployeeResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No employees found.",
                    data = null
                };
            }

            var employeeDtos = _objectMapper.Map<List<EmployeeResponseDto>>(employees);
            for (int i = 0; i < employees.Count; i++)
            {
                employeeDtos[i].Id = employees[i].Id;
                var userDto = new UserDto
                {
                    UserName = employees[i].User.UserName,
                    Name = employees[i].User.Name,
                    Surname = employees[i].User.Surname,
                    EmailAddress = employees[i].User.EmailAddress,
                    IsActive = employees[i].User.IsActive,
                    FullName = employees[i].User.FullName
                };
                employeeDtos[i].user = userDto;
            }

            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Employees retrieved successfully.",
                data = employeeDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public  async Task<ApiResponse<List<EmployeeResponseDto>>> GetAllEmployeeRequested()
    {
        try
        {
            var employees = await _employeeRepository.GetAllIncluding(m => m.User).
            Where(m=>m.IsApprovedByFounder == false)
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return new ApiResponse<List<EmployeeResponseDto>>
                {
                    status = false,
                    statusCode = 404,
                    message = "No employees found.",
                    data = null
                };
            }

            var employeeDtos = _objectMapper.Map<List<EmployeeResponseDto>>(employees);
            for (int i = 0; i < employees.Count; i++)
            {
                employeeDtos[i].Id = employees[i].Id;
                var userDto = new UserDto
                {
                    UserName = employees[i].User.UserName,
                    Name = employees[i].User.Name,
                    Surname = employees[i].User.Surname,
                    EmailAddress = employees[i].User.EmailAddress,
                    IsActive = employees[i].User.IsActive,
                    FullName = employees[i].User.FullName
                };
                employeeDtos[i].user = userDto;
            }

            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = true,
                statusCode = 200,
                message = "Employees retrieved successfully.",
                data = employeeDtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<EmployeeResponseDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employees: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<EmployeeResponseDto>> GetById(long id)
    {
        try
        {
            var employee = await _employeeRepository.GetAllIncluding(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Employee not found.",
                    data = null
                };
            }

            var employeeDto = _objectMapper.Map<EmployeeResponseDto>(employee);
            var userDto = new UserDto
            {
                UserName = employee.User.UserName,
                Name = employee.User.Name,
                Surname = employee.User.Surname,
                EmailAddress = employee.User.EmailAddress,
                IsActive = employee.User.IsActive,
                FullName = employee.User.FullName
            };
            employeeDto.user = userDto;
            return new ApiResponse<EmployeeResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee retrieved successfully.",
                data = employeeDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<EmployeeResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employee: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByUserId(long userId)
    {
        try
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(e => e.UserId == userId);

            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Employee not found for the given user ID.",
                    data = null
                };
            }

            var employeeDto = _objectMapper.Map<EmployeeResponseDto>(employee);
            employeeDto.Id = employee.Id;

            return new ApiResponse<EmployeeResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee retrieved successfully.",
                data = employeeDto
            };
        }
        
        catch (Exception ex)
        {
            return new ApiResponse<EmployeeResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving employee: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ApiResponse<EmployeeResponseDto>> RegisterEmployee(EmployeeDto dto)
    {


        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var tenantId = AbpSession.TenantId;
            var founder =  await _founderRepository.FirstOrDefaultAsync(f => f.TenantId == tenantId &&  f.UserName == "theFounder");

            if (user == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 401,
                    message = "the user is not exist please first create Account",
                    data = null
                };
            }
            await _userManager.AddToRoleAsync(user, "Employee");
            var employee = _objectMapper.Map<EmployeeDto, Employee>(dto);
            var response = _objectMapper.Map<EmployeeResponseDto>(employee);
            employee.UserId = user.Id;


            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 500,
                    message = "Somthing went wrong while  initialitation the Employee",
                    data = null
                };
            }
             employee.IsAppliedForEmploee = true;
             var result = await _employeeRepository.InsertAsync(employee);
             var userIdentifier = new UserIdentifier(_abpSession.TenantId, founder.UserId);
             await _notificationPublisher.PublishAsync(
              "LeaveApprovedNotification",
              new MessageNotificationData($"An user is Applied for a role of Employee with Name as {dto.FirstName} {dto.LastName}! "),
              userIds: new[] { userIdentifier }
             );

            if (result == null)
            {

                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 500,
                    message = "Somthing went wrong while adding  the Employee",
                    data = null
                };
            }
            dto.Id = result.Id;
            return new ApiResponse<EmployeeResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee added sucessfully",
                data = response
            };



        }
        catch (Exception err)
        {

            return new ApiResponse<EmployeeResponseDto>
            {
                status = false,
                statusCode = 500,
                message = err.Message,
                data = null
            };
        }
    }

    public async Task<ApiResponse<EmployeeResponseDto>> Update(EmployeeDto dto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "User not found for the given email.",
                    data = null
                };
            }

            var employee = await _employeeRepository.FirstOrDefaultAsync(e => e.UserId == user.Id);
            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Employee not found for the given user.",
                    data = null
                };
            }

            _objectMapper.Map(dto, employee); // Update fields
            await _employeeRepository.UpdateAsync(employee);

            var updatedDto = _objectMapper.Map<EmployeeResponseDto>(employee);
            updatedDto.Id = employee.Id;

            return new ApiResponse<EmployeeResponseDto>
            {
                status = true,
                statusCode = 200,
                message = "Employee updated successfully.",
                data = updatedDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<EmployeeResponseDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error updating employee: {ex.Message}",
                data = null
            };
        }
    }


}

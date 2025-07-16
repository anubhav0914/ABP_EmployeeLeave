using Abp.Domain.Repositories;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Services.Interfaces;
using EmployeeLeave.Utils;
using EmployeeLeave;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;

public class LeaveTypeServices : ApplicationService, ILeaveTypeServices
{
    private readonly IRepository<LeaveType, long> _leaveTypeRepository;
    private readonly IMapper _mapper;

    public LeaveTypeServices(IRepository<LeaveType, long> leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<LeaveTypeDto>> AddLeaveType(LeaveTypeDto dto)
    {
        try
        {
            var entity = _mapper.Map<LeaveType>(dto);
            var result = await _leaveTypeRepository.InsertAsync(entity);

            var resultDto = _mapper.Map<LeaveTypeDto>(result);

            return new ApiResponse<LeaveTypeDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave type added successfully.",
                data = resultDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveTypeDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error adding leave type: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<LeaveTypeDto>> Delete(long id)
    {
        try
        {
            var entity = await _leaveTypeRepository.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return new ApiResponse<LeaveTypeDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave type not found.",
                    data = null
                };
            }

            await _leaveTypeRepository.DeleteAsync(entity);

            return new ApiResponse<LeaveTypeDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave type deleted successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveTypeDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error deleting leave type: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<List<LeaveTypeDto>>> GetAll()
    {
        try
        {
            var entities = await _leaveTypeRepository.GetAllListAsync();
            var dtoList = _mapper.Map<List<LeaveTypeDto>>(entities);

            return new ApiResponse<List<LeaveTypeDto>>
            {
                status = true,
                statusCode = 200,
                message = "Leave types retrieved successfully.",
                data = dtoList
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<LeaveTypeDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving leave types: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<LeaveTypeDto>> GetById(long id)
    {
        try
        {
            var entity = await _leaveTypeRepository.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return new ApiResponse<LeaveTypeDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave type not found.",
                    data = null
                };
            }

            var dto = _mapper.Map<LeaveTypeDto>(entity);

            return new ApiResponse<LeaveTypeDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave type retrieved successfully.",
                data = dto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveTypeDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error retrieving leave type: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<LeaveTypeDto>> Update(LeaveTypeDto dto)
    {
        try
        {
            var entity = await _leaveTypeRepository.FirstOrDefaultAsync(dto.ID);
            if (entity == null)
            {
                return new ApiResponse<LeaveTypeDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave type not found.",
                    data = null
                };
            }

            _mapper.Map(dto, entity);
            await _leaveTypeRepository.UpdateAsync(entity);

            var updatedDto = _mapper.Map<LeaveTypeDto>(entity);

            return new ApiResponse<LeaveTypeDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave type updated successfully.",
                data = updatedDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveTypeDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error updating leave type: {ex.Message}",
                data = null
            };
        }
    }
}

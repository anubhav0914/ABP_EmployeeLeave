using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Services.Interfaces;
using EmployeeLeave.Utils;

namespace EmployeeLeave.Services.EntityServices;

public class LeaveRequestServices : ApplicationService, ILeaveRequestServices
{
    private readonly IRepository<LeaveRequest, long> _leaveRequestRepository;
    private readonly IMapper _mapper;

    public LeaveRequestServices(IRepository<LeaveRequest, long> leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<LeaveRequestCreateUpdateDto>> AddLeaveRequest(LeaveRequestCreateUpdateDto dto)
    {
        try 
        {
            if (dto == null)
            {
                return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave request added successfully.",
                data = null
            };
            }
            var entity = _mapper.Map<LeaveRequest>(dto);
            var result = await _leaveRequestRepository.InsertAsync(entity);
            
            var responseDto = _mapper.Map<LeaveRequestCreateUpdateDto>(result);
            responseDto.ID = result.Id;

            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave request added successfully.",
                data = responseDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ApiResponse<LeaveRequestCreateUpdateDto>> Delete(long id)
    {
        try
        {
            var entity = await _leaveRequestRepository.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return new ApiResponse<LeaveRequestCreateUpdateDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave request not found.",
                    data = null
                };
            }

            await _leaveRequestRepository.DeleteAsync(entity);

            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave request deleted successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ApiResponse<List<LeaveRequestCreateUpdateDto>>> GetAll()
    {
        try
        {
            var list = await _leaveRequestRepository.GetAllListAsync();
            var mapped = _mapper.Map<List<LeaveRequestCreateUpdateDto>>(list);
            for (int i = 0; i < list.Count; i++)
            {
                mapped[i].ID = list[i].Id;
            }
            return new ApiResponse<List<LeaveRequestCreateUpdateDto>>
            {
                status = true,
                statusCode = 200,
                message = "Leave requests retrieved successfully.",
                data = mapped
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<LeaveRequestCreateUpdateDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ApiResponse<LeaveRequestCreateUpdateDto>> GetById(long id)
    {
        try
        {
            var entity = await _leaveRequestRepository.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return new ApiResponse<LeaveRequestCreateUpdateDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave request not found.",
                    data = null
                };
            }

            var dto = _mapper.Map<LeaveRequestCreateUpdateDto>(entity);
            dto.ID = entity.Id;

            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave request retrieved successfully.",
                data = dto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ApiResponse<List<LeaveRequestCreateUpdateDto>>> GetByStatus(LeaveStatus statusId)
    {
        try
        {
            var list = await _leaveRequestRepository.GetAllListAsync(lr => lr.Status == statusId);
            var mapped = _mapper.Map<List<LeaveRequestCreateUpdateDto>>(list);
            
            for (int i = 0; i < list.Count; i++)
            {
                mapped[i].ID = list[i].Id;
            }
            return new ApiResponse<List<LeaveRequestCreateUpdateDto>>
            {
                status = true,
                statusCode = 200,
                message = "Leave requests filtered by status retrieved successfully.",
                data = mapped
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<LeaveRequestCreateUpdateDto>>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ApiResponse<LeaveRequestCreateUpdateDto>> Update(LeaveRequestCreateUpdateDto dto)
    {
        try
        {
            var entity = await _leaveRequestRepository.FirstOrDefaultAsync(dto.ID);
            if (entity == null)
            {
                return new ApiResponse<LeaveRequestCreateUpdateDto>
                {
                    status = false,
                    statusCode = 404,
                    message = "Leave request not found.",
                    data = null
                };
            }

            _mapper.Map(dto, entity);
            await _leaveRequestRepository.UpdateAsync(entity);

            var updatedDto = _mapper.Map<LeaveRequestCreateUpdateDto>(entity);
            updatedDto.ID = entity.Id;

            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = true,
                statusCode = 200,
                message = "Leave request updated successfully.",
                data = updatedDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LeaveRequestCreateUpdateDto>
            {
                status = false,
                statusCode = 500,
                message = $"Error: {ex.Message}",
                data = null
            };
        }
    }

}

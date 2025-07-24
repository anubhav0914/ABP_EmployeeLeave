using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using AutoMapper;
using EmployeeLeave.Domain.DTOs;
using EmployeeLeave.Model;
using EmployeeLeave.Services.Interfaces;
using EmployeeLeave.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace EmployeeLeave.Services.EntityServices;

public class LeaveRequestServices : ApplicationService, ILeaveRequestServices
{
    private readonly IRepository<LeaveRequest, long> _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly IRepository<Founder, long> _founderRepository;
    private readonly IRepository<Manager, long> _managerRepository;

    private readonly IAbpSession _abpSession;
    private readonly INotificationPublisher _notificationPublisher;


    public LeaveRequestServices(IRepository<LeaveRequest, long> leaveRequestRepository,
    IMapper mapper,
     IDistributedCache cache,
     IRepository<Founder, long> founderREpository,
     IRepository<Manager, long> managerREpository,

            INotificationPublisher notificationPublisher,
            IAbpSession abpSession
)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _cache = cache;
        _abpSession = abpSession;
        _managerRepository = managerREpository;
        _notificationPublisher = notificationPublisher;
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
            entity.TenantId = AbpSession.TenantId.Value;
            var result = await _leaveRequestRepository.InsertAsync(entity);
            var tenantId = AbpSession.TenantId;
            var founder = await _founderRepository.FirstOrDefaultAsync(f => f.TenantId == tenantId && f.UserName == "theFounder");
            var responseDto = _mapper.Map<LeaveRequestCreateUpdateDto>(result);
            responseDto.ID = result.Id;
            // notify founder and manager 
            var userIdentifiers = new List<UserIdentifier>();

            // Add founder
            userIdentifiers.Add(new UserIdentifier(_abpSession.TenantId, founder.UserId));

            // Get all managers (auto filtered by tenant due to IMustHaveTenant)
            var allManagers = await _managerRepository.GetAllListAsync();

            // Add managers
            userIdentifiers.AddRange(
                allManagers.Select(m => new UserIdentifier(_abpSession.TenantId, m.UserId))
            );

            // Publish notification to all (founder + managers)
            await _notificationPublisher.PublishAsync(
                "LeaveApprovedNotification",
                new MessageNotificationData($"A Leave request is received with the ID: {dto.EmployeeId}!"),
                userIds: userIdentifiers.ToArray()
            );

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
            var cacheKey = "leave_request_data";

            // 1. Try to get from cache
            var cachedValue = await _cache.GetStringAsync(cacheKey);
            var val = await _cache.GetStringAsync("leave_request_data");
            if (!string.IsNullOrEmpty(cachedValue))
            {
                var cachedList = JsonSerializer.Deserialize<List<LeaveRequestCreateUpdateDto>>(cachedValue);
                return new ApiResponse<List<LeaveRequestCreateUpdateDto>>
                {
                    status = true,
                    statusCode = 200,
                    message = "Leave requests retrieved from cache.",
                    data = cachedList
                };
            }

            // 2. Not found in cache, fetch from DB
            var list = await _leaveRequestRepository.GetAllListAsync();
            var mapped = _mapper.Map<List<LeaveRequestCreateUpdateDto>>(list);

            // 3. Ensure ID is assigned properly
            for (int i = 0; i < list.Count; i++)
            {
                mapped[i].ID = list[i].Id;
            }

            // 4. Serialize and store in Redis
            var serializedData = JsonSerializer.Serialize(mapped);
            await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            // 5. Return the response
            //  await Task.Delay(3000); // waits for 1 second (1000 ms)   did it for the testing of the cache 

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

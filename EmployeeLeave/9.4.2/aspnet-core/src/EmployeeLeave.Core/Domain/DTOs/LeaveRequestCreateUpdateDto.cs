using System;
using Abp.AutoMapper;

namespace EmployeeLeave.Domain.DTOs;

[AutoMapFrom(typeof(LeaveRequest))]
[AutoMapTo(typeof(LeaveRequest))]
public class LeaveRequestCreateUpdateDto
{
        public long ID { get; set; }
        public long EmployeeId { get; set; }
        public long LeaveTypeId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public long? Founder_Id_approved_rejected_BY { get; set; }

        public string? Reason { get; set; }

        public LeaveStatus Status { get; set; }
}

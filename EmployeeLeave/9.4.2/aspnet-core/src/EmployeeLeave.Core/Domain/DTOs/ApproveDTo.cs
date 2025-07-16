using System;

namespace EmployeeLeave.Domain.DTOs;

public class ApproveLeaveDto
{
   public long EmployeeId { get; set; }
   public long Approved_by_ID { get; set; }

   public long RequestId { get; set; }

   public LeaveStatus Status { get; set; }

}

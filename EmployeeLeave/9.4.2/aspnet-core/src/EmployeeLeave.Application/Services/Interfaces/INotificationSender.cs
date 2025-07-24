using System;
using System.Threading.Tasks;

namespace EmployeeLeave.Services.Interfaces;

public interface INotificationSender
{
     Task SendNotificationToUserAsync(Guid userId, string message);
}

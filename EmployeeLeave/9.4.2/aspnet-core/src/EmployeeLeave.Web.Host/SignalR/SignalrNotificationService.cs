using Abp.RealTime;
using Microsoft.AspNetCore.SignalR;
using Abp.AspNetCore.SignalR.Hubs;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Runtime.Session;

namespace EmployeeLeave.Web.Host.SignalR
{
    public class SignalrNotificationService
    {
        private readonly IOnlineClientManager _onlineClientManager;
        private readonly IHubContext<AbpCommonHub> _hubContext;
        private readonly IAbpSession _abpSession;

        public SignalrNotificationService(
            IOnlineClientManager onlineClientManager,
            IHubContext<AbpCommonHub> hubContext,
            IAbpSession abpSession)
        {
            _onlineClientManager = onlineClientManager;
            _hubContext = hubContext;
            _abpSession = abpSession;
        }

        public async Task NotifyUserAsync(long userId, string message)
        {
            var userIdentifier = new UserIdentifier(_abpSession.TenantId, userId);

            var onlineClients = await _onlineClientManager.GetAllByUserIdAsync(userIdentifier);

            foreach (var client in onlineClients)
            {
                await _hubContext.Clients.Client(client.ConnectionId)
                    .SendAsync("ReceiveMessage", message);
            }
        }
    }
}

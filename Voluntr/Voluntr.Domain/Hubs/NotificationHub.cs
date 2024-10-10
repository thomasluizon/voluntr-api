using Microsoft.AspNetCore.SignalR;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.Hubs
{
    public class NotificationHub(IUserConnectionManagerService userConnectionManagerService) : Hub
    {
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            userConnectionManagerService.KeepUserConnection(userId, connectionId);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            userConnectionManagerService.RemoveUserConnection(connectionId);

            return Task.CompletedTask;
        }
    }
}

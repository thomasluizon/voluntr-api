using Microsoft.AspNetCore.SignalR;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Hubs;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Services
{
    public class NotificationService(
        INotificationRepository notificationRepository,
        IHubContext<NotificationHub> notificationHubContext,
        IUserConnectionManagerService userConnectionManagerService,
        IUnitOfWork unitOfWork
    ) : INotificationService
    {
        public async Task SendNotification(Guid[] users, NotificationDto notification)
        {
            var createdNotification = await SaveNotification(users, notification);

            if (createdNotification != null)
            {
                List<string> connections = [];

                foreach (var userId in users)
                    connections = userConnectionManagerService.GetUserConnections(userId.ToString());

                if (connections?.Any() ?? false)
                {
                    foreach (var connectionId in connections)
                    {
                        notification.Id = createdNotification.Id;
                        notification.CreatedAt = createdNotification.CreatedAt;

                        await notificationHubContext.Clients
                            .Client(connectionId)
                            .SendAsync("sendNotification", notification);
                    }
                }
            }
        }

        private async Task<Notification> SaveNotification(Guid[] users, NotificationDto notification)
        {
            Notification newNotification = null;

            foreach (var userId in users)
            {
                newNotification = new Notification
                {
                    UserId = userId,
                    Level = notification.Level,
                    Title = notification.Title,
                    Url = notification.Url,
                    CreatedAt = DateTime.Now.ToBrazilianTimezone()
                };

                await notificationRepository.InsertAsync(newNotification);
            }

            if (await unitOfWork.CommitAsync())
                return newNotification;

            return null;
        }
    }
}

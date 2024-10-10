using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotification(Guid[] users, NotificationDto notification);
    }
}

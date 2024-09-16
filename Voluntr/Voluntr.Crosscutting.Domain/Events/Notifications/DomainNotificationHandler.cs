using MediatR;

namespace Voluntr.Crosscutting.Domain.Events.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> notifications = [];

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            GetNotifications().Add(message);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications() => notifications;

        public virtual bool HasNotifications() => GetNotifications().Count != 0;

        public void Dispose() => notifications = [];
    }
}

using MediatR;
using Voluntr.Crosscutting.Domain.Commands;
using Voluntr.Crosscutting.Domain.Events;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Queries;

namespace Voluntr.Crosscutting.Domain.MediatR
{
    public sealed class MediatorHandler(
        IMediator mediator,
        INotificationHandler<DomainNotification> domainNotification
    ) : IMediatorHandler
    {
        private readonly IMediator mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly DomainNotificationHandler domainNotification = (DomainNotificationHandler)domainNotification ??
                throw new ArgumentNullException(nameof(domainNotification));

        public Task SendCommand<T>(T command) where T : Command
        {
            return mediator.Send(command);
        }

        public Task<TResponse> SendCommandResponse<TResponse>(CommandResponse<TResponse> command) where TResponse : class
        {
            return mediator.Send(command);
        }

        public Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class
        {
            return mediator.Send(query);
        }

        public Task PublishEvent<T>(T @event) where T : Event
        {
            return mediator.Publish(@event);
        }

        public bool HasNotification()
        {
            return domainNotification.HasNotifications();
        }

        public INotificationHandler<DomainNotification> GetNotificationHandler()
        {
            return domainNotification;
        }
    }
}

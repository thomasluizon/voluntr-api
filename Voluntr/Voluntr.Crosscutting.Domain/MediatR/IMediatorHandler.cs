using MediatR;
using Voluntr.Crosscutting.Domain.Commands;
using Voluntr.Crosscutting.Domain.Events;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Queries;

namespace Voluntr.Crosscutting.Domain.MediatR
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task<TResponse> SendCommandResponse<TResponse>(CommandResponse<TResponse> command) where TResponse : class;
        Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class;
        Task PublishEvent<T>(T @event) where T : Event;
        bool HasNotification();
        INotificationHandler<DomainNotification> GetNotificationHandler();
    }
}

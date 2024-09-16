using MediatR;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Crosscutting.Domain.Commands.Handlers
{
    public abstract class MediatorCommandHandler<TCommand>(
        IMediatorHandler mediator
    ) : IRequestHandler<TCommand> where TCommand : Command
    {
        public abstract Task AfterValidation(TCommand request);

        public Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);

                return Task.CompletedTask;
            }

            return AfterValidation(request);
        }

        protected void NotifyValidationErrors(TCommand message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                mediator.PublishEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected Task NotifyWithError(string message)
        {
            NotifyError(message);

            return Task.CompletedTask;
        }

        protected void NotifyError(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                NotifyError(message);
            }
        }

        protected bool HasNotification() => mediator.HasNotification();

        protected IEnumerable<string> Errors => ((DomainNotificationHandler)mediator.GetNotificationHandler())
            .GetNotifications()
            .Select(t => t.Value);
    }
}

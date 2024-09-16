﻿using MediatR;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Crosscutting.Domain.Queries.Handlers
{
    public abstract class MediatorQueryHandler<TQuery, TResponse>(
        IMediatorHandler mediator
    ) : IRequestHandler<TQuery, TResponse> where TQuery
      : Query<TResponse> where TResponse : class
    {
        public abstract Task<TResponse> AfterValidation(TQuery request);

        public Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);

                return Task.FromResult<TResponse>(null);
            }

            return AfterValidation(request);
        }

        protected void NotifyValidationErrors(TQuery message)
        {
            foreach (var error in message.ValidationResult.Errors)
                mediator.PublishEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected void NotifyError(IEnumerable<string> messages) { foreach (var message in messages) NotifyError(message); }

        protected bool HasNotification() => mediator.HasNotification();

        protected IEnumerable<string> Errors => ((DomainNotificationHandler)mediator.GetNotificationHandler())
            .GetNotifications()
            .Select(t => t.Value);
    }
}

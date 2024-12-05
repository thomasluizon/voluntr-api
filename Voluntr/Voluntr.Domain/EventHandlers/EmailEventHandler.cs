using MediatR;
using Voluntr.Domain.Config;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Events;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.EventHandlers
{
    public class EmailEventHandler(
        IEmailService emailService,
        Urls urls
    ) : INotificationHandler<EmailActivationEvent>
    {
        public async Task Handle(EmailActivationEvent notification, CancellationToken cancellationToken)
        {
            await emailService.SendEmail(
                EmailTypeEnum.EmailVerification,
                notification.User.Name,
                notification.User.Email,
                new Dictionary<string, string>
                {
                    { "button-href", string.Format(urls.EmailActivation, notification.EmailActivationToken) }
                }
            );
        }
    }
}

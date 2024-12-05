using Voluntr.Crosscutting.Domain.Events;
using Voluntr.Domain.Config;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Events;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.EventHandlers
{
    public class EmailEventHandler(
        IEmailService emailService,
        Urls urls
    ) : IHandler<EmailActivationEvent>
    {
        public async void Handle(EmailActivationEvent message)
        {
            await emailService.SendEmail(
                EmailTypeEnum.EmailVerification,
                message.User.Name,
                message.User.Email,
                new Dictionary<string, string>
                {
                    { "button-href", string.Format(urls.EmailActivation, message.EmailActivationToken) }
                }
            );
        }
    }
}

using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Config;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class ResetPasswordRequestCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        Urls urls
    ) : MediatorResponseCommandHandler<ResetPasswordRequestCommand, AuthenticationDto>(mediator)
    {
        public async override Task<AuthenticationDto> AfterValidation(ResetPasswordRequestCommand request)
        {
            var user = await userRepository.GetFirstByExpressionAsync(
                x => x.Email == request.Email
            );

            if (user == null)
            {
                return null;
            }

            var resetToken = claimsService.GenerateResetToken(user);

            var buttonHref = string.Format(urls.ResetPassword, resetToken);
            var year = DateTime.Now.ToBrazilianTimezone().Year.ToString();

            await emailService.SendEmail(
                EmailTypeEnum.PasswordRecovery,
                user.Name,
                user.Email,
                new Dictionary<string, string>
                {
                    { "button-href", buttonHref },
                    { "year", year }
                }
            );

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}

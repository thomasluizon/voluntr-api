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
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class ResetPasswordRequestCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IResetPasswordTryRepository resetPasswordTryRepository,
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

            await resetPasswordTryRepository.DeleteByExpressionAsync(x => x.UserId == user.Id);

            var resetToken = claimsService.GenerateResetToken(user);

            var resetPasswordTry = new ResetPasswordTry
            {
                UserId = user.Id,
                ResetToken = resetToken
            };

            await resetPasswordTryRepository.InsertAsync(resetPasswordTry);

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

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
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}

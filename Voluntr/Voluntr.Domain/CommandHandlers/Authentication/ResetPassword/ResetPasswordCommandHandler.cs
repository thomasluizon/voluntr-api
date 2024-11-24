using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class ResetPasswordCommandHandler(
        IMediatorHandler mediator,
        IResetPasswordTryRepository resetPasswordTryRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IClaimsService claimsService
    ) : MediatorResponseCommandHandler<ResetPasswordCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(ResetPasswordCommand request)
        {
            var resetPassword = await resetPasswordTryRepository.GetFirstByExpressionAsync(
                x => x.ResetToken == request.Token,
                x => x.User
            );

            if (resetPassword == null)
            {
                NotifyError("A tentativa de redefinição de senha do usuário não foi encontrada");
                return null;
            }

            if (!claimsService.IsTokenValid(resetPassword.ResetToken)) 
            {
                NotifyError("O token de redefinição de senha informado não é mais válido");
                return null;
            }

            var user = resetPassword.User;
            user.Password = request.Password;

            await userRepository.UpdateAsync(user);

            await resetPasswordTryRepository.DeleteByExpressionAsync(x => x.UserId == user.Id);

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

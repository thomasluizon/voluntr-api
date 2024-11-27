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
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IClaimsService claimsService
    ) : MediatorResponseCommandHandler<ResetPasswordCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(ResetPasswordCommand request)
        {
            if (!claimsService.IsTokenValid(request.Token))
            {
                NotifyError("O token de redefinição de senha informado expirou ou não é valido");
                return null;
            }

            var user = await userRepository.GetByIdAsync(claimsService.GetUserIdFromToken(request.Token).Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return null;
            }

            user.Password = request.Password;

            await userRepository.UpdateAsync(user);

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

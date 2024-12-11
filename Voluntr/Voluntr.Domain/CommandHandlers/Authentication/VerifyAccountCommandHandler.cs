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
    public class VerifyAccountCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<VerifyAccountCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(VerifyAccountCommand request)
        {
            if (!claimsService.IsTokenValid(request.Token))
            {
                NotifyError("O token de ativação de conta informado expirou ou não é valido");
                return null;
            }

            var user = await userRepository.GetByIdAsync(claimsService.GetUserIdFromToken(request.Token).Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return null;
            }

            user.EmailVerified = true;

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

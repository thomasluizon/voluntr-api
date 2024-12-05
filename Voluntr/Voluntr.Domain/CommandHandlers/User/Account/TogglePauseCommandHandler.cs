using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class TogglePauseCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IUnitOfWork unitOfWork
    ) : MediatorCommandHandler<TogglePauseCommand>(mediator)
    {
        public override async Task AfterValidation(TogglePauseCommand request)
        {
            var user = await userRepository.GetByIdAsync(claimsService.GetCurrentUserId().Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return;
            }

            user.Paused = !user.Paused;

            await userRepository.UpdateAsync(user);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);
        }
    }
}

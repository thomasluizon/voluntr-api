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
    public class UpdatePasswordCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<UpdatePasswordCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(UpdatePasswordCommand request)
        {
            var user = await userRepository.GetByIdAsync(claimsService.GetCurrentUserId().Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return null;
            }

            if (user.Password != request.Password)
            {
                NotifyError("A senha atual informada é incorreta");
                return null;
            }

            user.Password = request.NewPassword;

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

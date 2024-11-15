using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class ResetPasswordRequestCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<ResetPasswordRequestCommand, AuthenticationDto>(mediator)
    {
        public async override Task<AuthenticationDto> AfterValidation(ResetPasswordRequestCommand request)
        {
            return null;
        }
    }
}

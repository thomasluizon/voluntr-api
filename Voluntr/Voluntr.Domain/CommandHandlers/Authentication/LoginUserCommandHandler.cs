using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class LoginUserCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<LoginUserCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(LoginUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}

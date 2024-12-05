using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class VerifyAccountCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<VerifyAccountCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(VerifyAccountCommand request)
        {
            return null;
        }
    }
}

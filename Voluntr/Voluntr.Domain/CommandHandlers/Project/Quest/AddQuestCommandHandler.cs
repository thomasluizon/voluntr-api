using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class AddQuestCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<AddQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(AddQuestCommand request)
        {
            return null;
        }
    }
}

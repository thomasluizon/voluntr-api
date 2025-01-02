using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateQuestCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<UpdateQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(UpdateQuestCommand request)
        {
            return null;
        }
    }
}

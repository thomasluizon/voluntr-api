using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class SubmitQuestCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<SubmitQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(SubmitQuestCommand request)
        {
            return null;
        }
    }
}

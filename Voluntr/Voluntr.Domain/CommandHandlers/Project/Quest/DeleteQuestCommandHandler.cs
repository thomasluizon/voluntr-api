using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class DeleteQuestCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<DeleteQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(DeleteQuestCommand request)
        {
            return null;
        }
    }
}

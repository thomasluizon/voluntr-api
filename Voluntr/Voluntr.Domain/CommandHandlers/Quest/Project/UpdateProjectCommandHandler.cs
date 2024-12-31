using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateProjectCommandHandler(
        IMediatorHandler mediator
    ) : MediatorResponseCommandHandler<UpdateProjectCommand, CommandResponseDto>(mediator)
    {
        public async override Task<CommandResponseDto> AfterValidation(UpdateProjectCommand request)
        {
            throw new NotImplementedException();
        }
    }
}

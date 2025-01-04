using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateVolunteerCommandHandler(
        IMediatorHandler mediator
    ) : MediatorCommandHandler<UpdateVolunteerCommand>(mediator)
    {
        public override Task AfterValidation(UpdateVolunteerCommand request)
        {
            throw new NotImplementedException();
        }
    }
}

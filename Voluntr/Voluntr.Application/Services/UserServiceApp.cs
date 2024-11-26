using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;

namespace Voluntr.Application.Services
{
    public class UserServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IUserServiceApp
    {
        public async Task ToggleUserPause()
        {
            var command = new ToggleUserPauseCommand();

            await mediator.SendCommand(command);
        }
    }
}

﻿using AutoMapper;
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
        public async Task TogglePause()
        {
            var command = new TogglePauseCommand();

            await mediator.SendCommand(command);
        }

        public async Task DeleteAccount()
        {
            var command = new DeleteAccountCommand();

            await mediator.SendCommand(command);
        }
    }
}

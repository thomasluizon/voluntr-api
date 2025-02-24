﻿using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;

namespace Voluntr.Application.Services
{
    public class AccountServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IAccountServiceApp
    {
        public async Task<AuthenticationResponseViewModel> Login(AuthenticationRequestViewModel viewModel)
        {
            var command = mapper.Map<LoginUserCommand>(viewModel);
            var response = await mediator.SendCommandResponse(command);

            return mapper.Map<AuthenticationResponseViewModel>(response);
        }

        public async Task<CommandResponseViewModel> Register(RegisterUserViewModel viewModel)
        {
            var command = mapper.Map<RegisterUserCommand>(viewModel);
            var response = await mediator.SendCommandResponse(command);

            return mapper.Map<CommandResponseViewModel>(response);
        }

        public async Task<AuthenticationResponseViewModel> OAuthLogin(OAuthAuthenticationRequestViewModel viewModel)
        {
            var command = mapper.Map<OAuthLoginUserCommand>(viewModel);
            var response = await mediator.SendCommandResponse(command);

            return mapper.Map<AuthenticationResponseViewModel>(response);
        }

        public async Task LinkOAuth(string OAuthProviderName)
        {
            var command = new LinkUserOAuthCommand { OAuthProviderName = OAuthProviderName };

            await mediator.SendCommandResponse(command);
        }

        public async Task ResetPasswordRequest(ResetPasswordRequestViewModel viewModel)
        {
            var command = mapper.Map<ResetPasswordRequestCommand>(viewModel);

            await mediator.SendCommandResponse(command);
        }

        public async Task ResetPassword(ResetPasswordViewModel viewModel)
        {
            var command = mapper.Map<ResetPasswordCommand>(viewModel);

            await mediator.SendCommandResponse(command);
        }

        public async Task UpdatePassword(UpdatePasswordViewModel viewModel)
        {
            var command = mapper.Map<UpdatePasswordCommand>(viewModel);

            await mediator.SendCommandResponse(command);
        }

        public async Task VerifyAccount(VerifyAccountViewModel viewModel)
        {
            var command = mapper.Map<VerifyAccountCommand>(viewModel);

            await mediator.SendCommandResponse(command);
        }
    }
}

using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;

namespace Voluntr.Application.Services
{
    public class AuthenticationServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IAuthenticationServiceApp
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
    }
}

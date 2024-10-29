using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IAuthenticationServiceApp
    {
        Task<AuthenticationResponseViewModel> Login(AuthenticationRequestViewModel viewModel);
        Task<CommandResponseViewModel> Register(RegisterUserViewModel viewModel);
        Task<AuthenticationResponseViewModel> HandleGoogleCallback();
    }
}

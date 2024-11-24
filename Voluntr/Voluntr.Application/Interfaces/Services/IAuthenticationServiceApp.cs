using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IAuthenticationServiceApp
    {
        Task<AuthenticationResponseViewModel> Login(AuthenticationRequestViewModel viewModel);
        Task<CommandResponseViewModel> Register(RegisterUserViewModel viewModel);
        Task<AuthenticationResponseViewModel> OAuthLogin(OAuthAuthenticationRequestViewModel viewModel);
        Task LinkOAuth(string OAuthProviderName);
        Task ResetPasswordRequest(ResetPasswordRequestViewModel viewModel);
        Task ResetPassword(ResetPasswordViewModel viewModel);
        Task UpdatePassword(UpdatePasswordViewModel viewModel);
    }
}

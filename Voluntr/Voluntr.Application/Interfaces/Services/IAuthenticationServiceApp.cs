using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IAuthenticationServiceApp
    {
        Task<CommandResponseViewModel> Register(RegisterUserViewModel viewModel);
    }
}

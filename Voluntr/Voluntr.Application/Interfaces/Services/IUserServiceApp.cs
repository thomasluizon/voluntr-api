using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IUserServiceApp
    {
        Task DeleteAccount();
        Task TogglePause();
        Task UploadPicture(UploadPictureRequestViewModel viewModel);
    }
}

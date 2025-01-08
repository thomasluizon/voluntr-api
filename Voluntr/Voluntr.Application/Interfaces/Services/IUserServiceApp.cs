using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IUserServiceApp
    {
        Task DeleteAccount();
        Task DeletePicture();
        Task TogglePause();
        Task UploadPicture(UploadPictureRequestViewModel viewModel);
    }
}

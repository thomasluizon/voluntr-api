using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IQuestServiceApp
    {
        Task<CommandResponseViewModel> CreateProject(ProjectRequestViewModel viewModel, bool update = false);
    }
}

using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IProjectServiceApp
    {
        Task<CommandResponseViewModel> CreateQuest(QuestRequestViewModel viewModel, string projectId, bool update = false);
        Task<CommandResponseViewModel> CreateProject(ProjectRequestViewModel viewModel, bool update = false);
        Task DeleteProject(string id);
        Task DeleteQuest(string projectId, string questId);
        Task AssignQuest(string projectId, string questId);
        Task SubmitQuest(string projectId, string questId, SubmitQuestViewModel viewModel);
    }
}

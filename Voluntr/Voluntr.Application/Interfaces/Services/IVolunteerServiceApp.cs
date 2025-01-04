using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IVolunteerServiceApp
    {
        Task<List<OnboardingTaskViewModel>> GetOnboarding();
        Task<VolunteerProfileViewModel> GetProfile();
        Task UpdateVolunteer(VolunteerRequestViewModel viewModel);
    }
}

using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Queries;

namespace Voluntr.Application.Services
{
    public class VolunteerServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IVolunteerServiceApp
    {
        public async Task<List<OnboardingTaskViewModel>> GetOnboarding()
        {
            var query = new GetVolunteerOnboardingQuery();
            var response = await mediator.SendQuery(query);

            return mapper.Map<List<OnboardingTaskViewModel>>(response);
        }

        public async Task<VolunteerProfileViewModel> GetProfile()
        {
            var query = new GetVolunteerProfileQuery();
            var response = await mediator.SendQuery(query);

            return mapper.Map<VolunteerProfileViewModel>(response);
        }

        public async Task UpdateVolunteer(VolunteerRequestViewModel viewModel)
        {
            var command = mapper.Map<UpdateVolunteerCommand>(viewModel);

            await mediator.SendCommand(command);
        }
    }
}
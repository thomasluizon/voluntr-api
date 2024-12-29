using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Queries.Achievement;

namespace Voluntr.Application.Services
{
    public class CauseServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : ICauseServiceApp
    {
        public async Task<AchievementsPageViewModel> GetAchievementsPage()
        {
            var query = new GetAchievementsPageQuery();

            var response = await mediator.SendQuery(query);

            return mapper.Map<AchievementsPageViewModel>(response);
        }
    }
}

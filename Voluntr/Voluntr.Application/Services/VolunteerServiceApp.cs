using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Queries;

namespace Voluntr.Application.Services
{
    public class VolunteerServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IVolunteerServiceApp
    {
        public async Task<List<VolunteerResponseViewModel>> GetVolunteers()
        {
            var query = new GetVolunteersQuery();
            var response = await mediator.SendQuery(query);

            return mapper.Map<List<VolunteerResponseViewModel>>(response);
        }
    }
}

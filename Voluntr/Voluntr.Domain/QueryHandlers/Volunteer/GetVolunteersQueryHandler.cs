using AutoMapper;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Queries;

namespace Voluntr.Domain.QueryHandlers
{
    public class GetVolunteersQueryHandler(
        IMediatorHandler mediator,
        IMapper mapper,
        IVolunteerRepository volunteerRepository
    ) : MediatorQueryHandler<GetVolunteersQuery, List<VolunteerDto>>(mediator)
    {
        public override async Task<List<VolunteerDto>> AfterValidation(GetVolunteersQuery request)
        {
            var response = await volunteerRepository.ListAllAsync();

            if (response == null)
            {
                return [];
            }

            return mapper.Map<List<VolunteerDto>>(response);
        }
    }
}

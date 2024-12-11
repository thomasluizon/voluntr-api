using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Queries;

namespace Voluntr.Application.Services
{
    public class ExternalServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IExternalServiceApp
    {
        public async Task<List<string>> GetCities(string uf)
        {
            var query = new GetCitiesQuery
            {
                Uf = uf
            };

            return await mediator.SendQuery(query);
        }

        public async Task<List<string>> GetUfs()
        {
            var query = new GetUfsQuery();

            return await mediator.SendQuery(query);
        }

        public async Task<ZipCodeInformationViewModel> GetZipCodeInformation(string zipCode)
        {
            var query = new GetZipCodeInformationQuery
            {
                ZipCode = zipCode
            };

            var response = await mediator.SendQuery(query);

            return mapper.Map<ZipCodeInformationViewModel>(response);
        }
    }
}

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

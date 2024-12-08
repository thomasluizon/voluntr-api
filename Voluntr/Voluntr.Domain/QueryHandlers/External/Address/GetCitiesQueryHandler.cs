using System.Text.Json;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.Config;
using Voluntr.Domain.Queries;

namespace Voluntr.Domain.QueryHandlers
{
    public class GetCitiesQueryHandler(
        IMediatorHandler mediator,
        HttpClient httpClient,
        Urls urls
    ) : MediatorQueryHandler<GetCitiesQuery, List<string>>(mediator)
    {
        public override async Task<List<string>> AfterValidation(GetCitiesQuery request)
        {
            var apiResponse = await httpClient.GetAsync(string.Format(urls.Cities, request.Uf));

            if (!apiResponse.IsSuccessStatusCode)
            {
                NotifyError("Ocorreu um erro ao buscar as cidades");
                return null;
            }

            var jsonString = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonDocument
                .Parse(jsonString)
                .RootElement
                .EnumerateArray()
                .Select(city => city.GetProperty("municipio").GetProperty("nome").GetString())
                .Distinct()
                .ToList();

            if (response.Count == 0)
            {
                NotifyError("O UF informado é inválido");
                return null;
            }

            return response;
        }
    }
}
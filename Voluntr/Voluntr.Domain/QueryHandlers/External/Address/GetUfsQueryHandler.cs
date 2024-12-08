using System.Text.Json;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.Config;
using Voluntr.Domain.Queries;

namespace Voluntr.Domain.QueryHandlers
{
    public class GetUfsQueryHandler(
        IMediatorHandler mediator,
        HttpClient httpClient,
        Urls urls
    ) : MediatorQueryHandler<GetUfsQuery, List<string>>(mediator)
    {
        public override async Task<List<string>> AfterValidation(GetUfsQuery request)
        {
            var response = await httpClient.GetAsync(urls.Uf);

            if (!response.IsSuccessStatusCode)
            {
                NotifyError("Ocorreu um erro ao buscar os UFs");
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonDocument
                .Parse(jsonString)
                .RootElement
                .EnumerateArray()
                .Select(uf => uf.GetProperty("sigla").GetString())
                .ToList();
        }
    }
}

using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.Config;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Queries;

namespace Voluntr.Domain.QueryHandlers
{
    public partial class GetZipCodeInformationQueryHandler(
        IMediatorHandler mediator,
        HttpClient httpClient,
        Urls urls
    ) : MediatorQueryHandler<GetZipCodeInformationQuery, ZipCodeInformationDto>(mediator)
    {
        public override async Task<ZipCodeInformationDto> AfterValidation(GetZipCodeInformationQuery request)
        {
            var normalizedZipCode = ValidateAndNormalizeZipCode(request.ZipCode);

            var zipCodeError = "O CEP informado é inválido";

            if (string.IsNullOrEmpty(normalizedZipCode))
            {
                NotifyError(zipCodeError);
                return null;
            }

            var apiRequest = await httpClient.GetAsync(string.Format(urls.ViaCep, normalizedZipCode));

            if (!apiRequest.IsSuccessStatusCode)
            {
                NotifyError("Erro ao buscar o CEP informado, por favor informe seu endereço manualmente");
                return null;
            }

            var response = await apiRequest.Content.ReadFromJsonAsync<ZipCodeInformationDto>();

            if (string.IsNullOrEmpty(response.Street))
            {
                NotifyError(zipCodeError);
                return null;
            }

            return response;
        }

        private static string ValidateAndNormalizeZipCode(string zipCode)
        {
            zipCode = zipCode?.Trim();

            var isValid = ZipCodeValidator.ValidZipCodeRegex().IsMatch(zipCode);

            if (!isValid)
            {
                return null;
            }

            return zipCode.Replace("-", string.Empty);
        }

        private static partial class ZipCodeValidator
        {
            [GeneratedRegex(@"^\d{5}-?\d{3}$")]
            public static partial Regex ValidZipCodeRegex();
        }
    }
}

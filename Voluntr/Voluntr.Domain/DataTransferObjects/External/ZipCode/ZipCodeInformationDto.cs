using System.Text.Json.Serialization;

namespace Voluntr.Domain.DataTransferObjects
{
    public class ZipCodeInformationDto
    {
        [JsonPropertyName("logradouro")]
        public string Street { get; set; }

        [JsonPropertyName("bairro")]
        public string Neighbourhood { get; set; }

        [JsonPropertyName("uf")]
        public string State { get; set; }

        [JsonPropertyName("localidade")]
        public string City { get; set; }
    }
}

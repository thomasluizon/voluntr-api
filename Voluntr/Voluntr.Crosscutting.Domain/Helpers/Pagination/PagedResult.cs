using System.Text.Json.Serialization;

namespace Voluntr.Crosscutting.Domain.Helpers.Pagination
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        /// <summary>
        /// Registros paginados
        /// </summary>
        [JsonPropertyName("results")]
        public IList<T> Results { get; set; } = [];

        /// <summary>
        /// Total de registros da consulta
        /// </summary>
        [JsonPropertyName("result_count")]
        public int ResultCount
        {
            get { return Results.Count; }
        }
    }
}

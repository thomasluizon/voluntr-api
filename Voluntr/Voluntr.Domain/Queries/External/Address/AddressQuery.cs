using Voluntr.Crosscutting.Domain.Queries;

namespace Voluntr.Domain.Queries
{
    public abstract class AddressQuery<TResponse> : Query<TResponse>
    {
        public string ZipCode { get; set; }
        public string Uf { get; set; }
    }
}

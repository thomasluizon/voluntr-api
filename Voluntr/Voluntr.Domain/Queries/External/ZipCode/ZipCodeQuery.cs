using Voluntr.Crosscutting.Domain.Queries;

namespace Voluntr.Domain.Queries
{
    public abstract class ZipCodeQuery<TResponse> : Query<TResponse>
    {
        public string ZipCode { get; set; }
    }
}

namespace Voluntr.Domain.Queries
{
    public class GetCitiesQuery : AddressQuery<List<string>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}

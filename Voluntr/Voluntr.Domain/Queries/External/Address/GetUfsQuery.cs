namespace Voluntr.Domain.Queries
{
    public class GetUfsQuery : AddressQuery<List<string>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}

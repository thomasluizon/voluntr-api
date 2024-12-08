using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetZipCodeInformationQuery : AddressQuery<ZipCodeInformationDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}

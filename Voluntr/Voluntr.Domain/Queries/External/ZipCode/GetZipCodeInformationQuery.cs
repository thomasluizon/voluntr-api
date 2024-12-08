using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetZipCodeInformationQuery : ZipCodeQuery<ZipCodeInformationDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}

using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class OAuthProvider : Entity
    {
        public string Name { get; set; }
        public string UserInfoApiUrl { get; set; }

        public string EmailProperty { get; set; }
        public string NameProperty { get; set; }
        public string PictureProperty { get; set; }

        public OAuthProviderNameEnum NameEnum
        {
            get { return EnumExtension.GetEnumerator<OAuthProviderNameEnum>(Name?.Trim()); }
            set { Name = value.GetDescription(); }
        }
    }
}
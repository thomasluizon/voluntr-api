using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool OAuth { get; set; }
        public string OAuthProvider { get; set; }

        public OAuthProviderEnum OAuthProviderEnum
        {
            get { return EnumExtension.GetEnumerator<OAuthProviderEnum>(OAuthProvider?.Trim()); }
            set { OAuthProvider = value.GetDescription(); }
        }
    }
}

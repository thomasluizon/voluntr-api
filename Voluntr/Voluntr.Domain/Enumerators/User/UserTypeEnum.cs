using System.ComponentModel;

namespace Voluntr.Domain.Enumerators
{
    public enum UserTypeEnum
    {
        [Description("Voluntário")]
        Volunteer,

        [Description("Ong")]
        Ngo,

        [Description("Empresa")]
        Company
    }
}

using System.ComponentModel;

namespace Voluntr.Domain.Enumerators
{
    public enum NotificationLevelEnum
    {
        [Description("Sucesso")]
        Success,

        [Description("Aviso")]
        Warning,

        [Description("Critico")]
        Danger
    }
}

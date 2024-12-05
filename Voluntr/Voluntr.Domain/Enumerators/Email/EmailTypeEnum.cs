using System.ComponentModel;

namespace Voluntr.Domain.Enumerators
{
    public enum EmailTypeEnum
    {
        [Description("Verificação de email")]
        EmailVerification,

        [Description("Recuperação de senha")]
        PasswordRecovery,
    }
}

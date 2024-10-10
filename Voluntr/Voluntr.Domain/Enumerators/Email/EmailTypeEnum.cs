using System.ComponentModel;

namespace Voluntr.Domain.Enumerators
{
    public enum EmailTypeEnum
    {
        [Description("Criação de conta")]
        CreateAccount,

        [Description("Recuperação de senha")]
        PasswordRecovery,

        [Description("Autenticação de dois fatores")]
        TwoFactorAuthentication
    }
}

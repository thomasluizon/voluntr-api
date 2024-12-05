using FluentValidation;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Validations
{
    public class AccountValidation<TCommand, TResponse>
        : AbstractValidator<TCommand> where TCommand : AccountCommand<TResponse>
    {
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotNull().NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("O email informado não é valido")
                .MaximumLength(100).WithMessage("O email deve conter no máximo 100 caracteres");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotNull().NotEmpty().WithMessage("A senha é obrigatória")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A senha deve conter no máximo 100 caracteres");
        }

        protected void ValidateNewPassword()
        {
            RuleFor(c => c.NewPassword)
                .NotNull().NotEmpty().WithMessage("A nova senha é obrigatória")
                .MinimumLength(6).WithMessage("A nova senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A nova senha deve conter no máximo 100 caracteres");
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotNull().NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .MaximumLength(100).WithMessage("O nome do usuário deve conter no máximo 100 caracteres");
        }

        protected void ValidateToken()
        {
            RuleFor(c => c.Token)
                .NotNull().NotEmpty().WithMessage("O token é obrigatório");
        }

        #region OAuth

        protected void ValidateOAuthToken()
        {
            RuleFor(c => c.OAuthToken)
                .NotNull().NotEmpty().WithMessage("O token é obrigatório");
        }

        protected void ValidateOAuthProvider()
        {
            RuleFor(c => c.OAuthProviderName)
                .NotNull().NotEmpty().WithMessage("O provedor de autenticação é obrigatório")
                .Must(ValidateOAuthProvider).WithMessage("Não é possível fazer login com o provedor de autenticação informado");
        }

        private bool ValidateOAuthProvider(string provider)
        {
            return provider.Trim() == OAuthProviderNameEnum.Google.GetDescription();
        }

        #endregion
    }
}
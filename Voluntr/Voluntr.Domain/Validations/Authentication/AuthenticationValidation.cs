using FluentValidation;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class AuthenticationValidation<TCommand, TResponse>
        : AbstractValidator<TCommand> where TCommand : AuthenticationCommand<TResponse>
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

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotNull().NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .MaximumLength(100).WithMessage("O nome do usuário deve conter no máximo 100 caracteres");
        }
    }
}
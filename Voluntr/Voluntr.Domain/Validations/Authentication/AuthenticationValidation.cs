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
                .EmailAddress().WithMessage("O email informado não é valido");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotNull().NotEmpty().WithMessage("A senha é obrigatória")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres");
        }
    }
}
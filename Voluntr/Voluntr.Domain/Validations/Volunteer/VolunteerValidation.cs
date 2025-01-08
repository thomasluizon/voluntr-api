using FluentValidation;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Validations.User;

namespace Voluntr.Domain.Validations
{
    public partial class VolunteerValidation<TCommand> : AbstractValidator<TCommand> where TCommand : VolunteerCommand
    {
        protected void ValidateVolunteer()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("O sobrenome é obrigatório.")
                .MaximumLength(100).WithMessage("O sobrenome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Nickname)
                .MaximumLength(20).WithMessage("O apelido deve ter no máximo 20 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Nickname));

            RuleFor(x => x.Phone)
                .Must(ValidatePhoneNumber)
                .WithMessage("O telefone deve conter exatamente 11 números.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("O endereço é obrigatório.")
                .SetValidator(new AddressValidator());
        }

        private bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return true;

            var numericPhone = new string(phone.Where(char.IsDigit).ToArray());

            return numericPhone.Length == 11;
        }
    }
}

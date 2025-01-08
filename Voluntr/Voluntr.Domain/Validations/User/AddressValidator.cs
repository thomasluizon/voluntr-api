using FluentValidation;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations.User
{
    public class AddressValidator : AbstractValidator<AddressCommand>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("A rua do endereço é obrigatória.")
                .MaximumLength(200).WithMessage("A rua do endereço deve ter no máximo 200 caracteres.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("A cidade do endereço é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade do endereço deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("O número do endereço é obrigatório.")
                .MaximumLength(10).WithMessage("O número do endereço deve ter no máximo 10 caracteres.");

            RuleFor(x => x.Complement)
                .MaximumLength(100).WithMessage("O complemento do endereço deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Neighbourhood)
                .NotNull().NotEmpty().WithMessage("O bairro é obrigatório.")
                .MaximumLength(100).WithMessage("A cidade do endereço deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("O estado do endereço é obrigatório.")
                .MaximumLength(2).WithMessage("O estado do endereço deve ter no máximo 2 caracteres.")
                .Matches(@"^[A-Z]{2}$").WithMessage("O estado deve conter exatamente 2 letras maiúsculas.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("O CEP do endereço é obrigatório.")
                .Matches(@"^\d{5}-?\d{3}$").WithMessage("O CEP deve estar no formato 12345-678 ou 12345678.");
        }
    }
}

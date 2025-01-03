using FluentValidation;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class ProjectValidation<TCommand> : AbstractValidator<TCommand> where TCommand : ProjectCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotEqual(Guid.Empty).WithMessage("O código do projeto é obrigatório");
        }

        protected void ValidateProject()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("O nome do projeto é obrigatório")
                .MaximumLength(50).WithMessage("O nome do projeto precisa ter no máximo 50 caracteres");

            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("A descrição do projeto é obrigatória")
                .MaximumLength(250).WithMessage("A descrição do projeto precisa ter no máximo 250 caracteres");

            RuleFor(x => x.DueDate)
                .Must(ValidateDueDate).WithMessage("A data final do projeto precisa ser no futuro");
        }

        private bool ValidateDueDate(DateTime? dueDate)
        {
            if (!dueDate.HasValue)
                return true;

            if (dueDate.Value < DateTime.Now.ToBrazilianTimezone())
                return false;

            return true;
        }
    }
}

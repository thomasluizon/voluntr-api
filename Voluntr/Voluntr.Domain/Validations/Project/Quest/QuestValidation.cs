using FluentValidation;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class QuestValidation<TCommand> : AbstractValidator<TCommand> where TCommand : QuestCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotEqual(Guid.Empty).WithMessage("O código da tarefa é obrigatório");

            RuleFor(x => x.ProjectId)
                .NotEmpty().NotEqual(Guid.Empty).WithMessage("O código do projeto é obrigatório");
        }

        protected void ValidateQuest()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().NotEqual(Guid.Empty).WithMessage("O código do projeto é obrigatório");

            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("O nome da tarefa é obrigatório")
                .MaximumLength(50).WithMessage("O nome da tarefa precisa ter no máximo 50 caracteres");

            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("A descrição da tarefa é obrigatória")
                .MaximumLength(250).WithMessage("A descrição da tarefa precisa ter no máximo 250 caracteres");

            RuleFor(x => x.DueDate)
                .Must(ValidateDueDate).WithMessage("A data final da tarefa precisa ser no futuro");

            RuleFor(x => x.Reward)
                .NotEmpty().WithMessage("A recompensa da tarefa é necessária")
                .Must(x => x >= 0).WithMessage("A recompensa da tarefa não pode ser negativa");
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

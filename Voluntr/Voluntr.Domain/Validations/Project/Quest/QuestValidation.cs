using FluentValidation;
using Microsoft.AspNetCore.Http;
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
                .MaximumLength(250).WithMessage("A descrição da tarefa precisa ter no máximo 250 caracteres");

            RuleFor(x => x.DueDate)
                .Must(ValidateDueDate).WithMessage("A data final da tarefa precisa ser no futuro");

            RuleFor(x => x.Reward)
                .NotEmpty().WithMessage("A recompensa da tarefa é necessária")
                .Must(x => x >= 0).WithMessage("A recompensa da tarefa não pode ser negativa");
        }

        protected void ValidateSubmission()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage("O arquivo da imagem é obrigatório.")
                .Must(x => x.Length > 0).WithMessage("O arquivo enviado está vazio.")
                .Must(IsValidExtension).WithMessage("Apenas arquivos .png, .jpg ou .jpeg são suportados.")
                .Must(IsValidSize).WithMessage("O arquivo enviado excede o tamanho máximo permitido de 32MB.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("A descrição da submissão precisa ter no máximo 250 caracteres");
        }

        private bool IsValidExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName)?.ToLower();
            return extension == ".png" || extension == ".jpg" || extension == ".jpeg";
        }

        private bool IsValidSize(IFormFile file)
        {
            const long maxSizeInBytes = 32 * 1024 * 1024;
            return file.Length <= maxSizeInBytes;
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

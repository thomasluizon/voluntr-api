using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class DeleteQuestCommand : QuestCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new DeleteQuestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}

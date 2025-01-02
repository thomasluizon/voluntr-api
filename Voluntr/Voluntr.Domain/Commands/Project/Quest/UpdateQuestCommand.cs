using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class UpdateQuestCommand : QuestCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateQuestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}

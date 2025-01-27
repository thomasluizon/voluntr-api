using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class AssignQuestCommand : QuestCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AssignQuestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}

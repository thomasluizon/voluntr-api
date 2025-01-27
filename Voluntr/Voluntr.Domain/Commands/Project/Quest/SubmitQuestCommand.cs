using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class SubmitQuestCommand : QuestCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new SubmitQuestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}

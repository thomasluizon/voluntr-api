using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class AddQuestCommand : QuestCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddQuestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}

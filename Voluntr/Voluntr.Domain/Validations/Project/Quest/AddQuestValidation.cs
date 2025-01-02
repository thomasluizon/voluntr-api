using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class AddQuestValidation : QuestValidation<AddQuestCommand>
    {
        public AddQuestValidation()
        {
            ValidateQuest();
        }
    }
}

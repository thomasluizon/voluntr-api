using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UpdateQuestValidation : QuestValidation<UpdateQuestCommand>
    {
        public UpdateQuestValidation()
        {
            ValidateId();
            ValidateQuest();
        }
    }
}

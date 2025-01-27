using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class AssignQuestValidation : QuestValidation<AssignQuestCommand>
    {
        public AssignQuestValidation()
        {
            ValidateId();
        }
    }
}

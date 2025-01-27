using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class SubmitQuestValidation : QuestValidation<SubmitQuestCommand>
    {
        public SubmitQuestValidation()
        {
            ValidateId();
            ValidateSubmission();
        }
    }
}

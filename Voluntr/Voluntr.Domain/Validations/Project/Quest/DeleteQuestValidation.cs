using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class DeleteQuestValidation : QuestValidation<DeleteQuestCommand>
    {
        public DeleteQuestValidation()
        {
            ValidateId();
        }
    }
}

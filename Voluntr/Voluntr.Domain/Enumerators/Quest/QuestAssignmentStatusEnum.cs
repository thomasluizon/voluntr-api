using System.ComponentModel;

namespace Voluntr.Domain.Enumerators
{
    public enum QuestAssignmentStatusEnum
    {
        [Description("Pendente")]
        Pending,

        [Description("Enviada")]
        Submitted,

        [Description("Aprovada")]
        Approved,

        [Description("Rejeitada")]
        Rejected
    }
}

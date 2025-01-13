using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class QuestAssignment : Entity
    {
        public Guid QuestId { get; set; }
        public Guid VolunteerId { get; set; }

        public string Status { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ImageAttachmentUrl { get; set; }
        public string Description { get; set; }
        public string SubmissionResponse { get; set; }

        public QuestAssignmentStatusEnum QuestAssignmentStatusEnum
        {
            get { return EnumExtension.GetEnumerator<QuestAssignmentStatusEnum>(Status?.Trim()); }
            set { Status = value.GetDescription(); }
        }

        public virtual Quest Quest { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}

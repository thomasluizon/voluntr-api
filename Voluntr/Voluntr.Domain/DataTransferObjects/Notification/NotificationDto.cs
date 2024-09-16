using Voluntr.Crosscutting.Domain.Helpers.Extensions;

namespace Voluntr.Domain.DataTransferObjects
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Level { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedAt { get; set; }

        public string FriendlyDate
        {
            get { return CreatedAt.ToFriendlyDateTimeString(); }
        }
    }
}

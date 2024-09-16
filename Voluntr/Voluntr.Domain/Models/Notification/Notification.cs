using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class Notification : Entity
    {
        // Definir estrutura de Id externo

        public string Title { get; set; }
        public string Url { get; set; }
        public string Level { get; set; }

        public NotificationLevelEnum NotificationLevelEnum
        {
            get { return EnumExtension.GetEnumerator<NotificationLevelEnum>(Level?.Trim()); }
            set { Level = value.GetDescription(); }
        }
    }
}

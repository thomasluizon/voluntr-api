using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class Email : Entity
    {
        public string TemplateId { get; set; }
        public string Type { get; set; }

        public EmailTypeEnum EmailTypeEnum
        {
            get { return EnumExtension.GetEnumerator<EmailTypeEnum>(Type?.Trim()); }
            set { Type = value.GetDescription(); }
        }
    }
}

using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Models;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Models
{
    public class OnboardingTask : Entity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Redirect { get; set; }

        public OnboardingTaskEnum OnboardingTaskEnum
        {
            get { return EnumExtension.GetEnumerator<OnboardingTaskEnum>(Type?.Trim()); }
            set { Type = value.GetDescription(); }
        }
    }
}

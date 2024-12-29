using Voluntr.Crosscutting.Domain.Queries;

namespace Voluntr.Domain.Queries.Achievement
{
    public abstract class AchievementQuery<TResponse> : Query<TResponse>
    {
        public Guid CauseId { get; set; }
    }
}

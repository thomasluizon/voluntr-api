namespace Voluntr.Domain.DataTransferObjects
{
    public class CauseAchievementsPageDto
    {
        public int NumberOfQuestsToNextAchievement { get; set; }
        public List<AchievementForCauseAchievementsPageDto> Achievements { get; set; }
    }

    public class AchievementForCauseAchievementsPageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}

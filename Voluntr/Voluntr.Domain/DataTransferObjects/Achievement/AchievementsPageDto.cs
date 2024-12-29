namespace Voluntr.Domain.DataTransferObjects
{
    public class AchievementsPageDto
    {
        public List<AchievementForAchievementsPageDto> Achievements { get; set; }
        public List<CauseForAchievementsPageDto> Causes { get; set; }
    }

    public class AchievementForAchievementsPageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Done { get; set; }
    }

    public class CauseForAchievementsPageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CompletedAchievements { get; set; }
        public int TotalAchievements { get; set; }
    }
}

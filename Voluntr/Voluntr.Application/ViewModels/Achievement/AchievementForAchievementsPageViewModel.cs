namespace Voluntr.Application.ViewModels
{
    public class AchievementsPageViewModel
    {
        public List<AchievementForAchievementsPageViewModel> Achievements { get; set; }
        public List<CauseForAchievementsPageViewModel> Causes { get; set; }
    }

    public class AchievementForAchievementsPageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Done { get; set; }
    }

    public class CauseForAchievementsPageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CompletedAchievements { get; set; }
        public int TotalAchievements { get; set; }
    }
}

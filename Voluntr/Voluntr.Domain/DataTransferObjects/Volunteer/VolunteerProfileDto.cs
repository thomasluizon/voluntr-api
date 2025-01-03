namespace Voluntr.Domain.DataTransferObjects
{
    public class VolunteerProfileDto
    {
        public VolunteerInformationDto Volunteer { get; set; }
        public List<AchievementForAchievementsPageDto> Achievements { get; set; }
        public int TotalCoins { get; set; }
        public int QuestsCompleted { get; set; }
    }

    public class VolunteerInformationDto
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string RegisterYear { get; set; }
        public string ImageUrl { get; set; }
    }
}

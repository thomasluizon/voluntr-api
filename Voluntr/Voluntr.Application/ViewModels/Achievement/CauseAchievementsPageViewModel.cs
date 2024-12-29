namespace Voluntr.Application.ViewModels
{
    public class CauseAchievementsPageViewModel
    {
        /// <summary>
        /// Quantidade de tarefas necessárias para a próxima conquista
        /// </summary>
        public int NumberOfQuestsToNextAchievement { get; set; }

        /// <summary>
        /// Conquistas da causa
        /// </summary>
        public List<AchievementForCauseAchievementsPageViewModel> Achievements { get; set; }
    }

    public class AchievementForCauseAchievementsPageViewModel
    {
        /// <summary>
        /// Código da conquista
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da conquista
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url da imagem da conquista
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Descrição da conquista
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Flag que indica se a conquista já foi adquirida pelo voluntário
        /// </summary>
        public bool Done { get; set; }
    }
}

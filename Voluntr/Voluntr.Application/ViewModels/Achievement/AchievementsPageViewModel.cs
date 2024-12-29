namespace Voluntr.Application.ViewModels
{
    public class AchievementsPageViewModel
    {
        /// <summary>
        /// Conquistas gerais
        /// </summary>
        public List<AchievementForAchievementsPageViewModel> Achievements { get; set; }

        /// <summary>
        /// Causas
        /// </summary>
        public List<CauseForAchievementsPageViewModel> Causes { get; set; }
    }

    public class AchievementForAchievementsPageViewModel
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
        /// Flag que indica se a conquista já foi adquirida pelo voluntário
        /// </summary>
        public bool Done { get; set; }
    }

    public class CauseForAchievementsPageViewModel
    {
        /// <summary>
        /// Código da causa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da causa
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url da imagem da causa
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Quantidade de conquistas adquiridas para esta causa pelo voluntário
        /// </summary>
        public int CompletedAchievements { get; set; }

        /// <summary>
        /// Quantidade de conquistas totais para esta causa
        /// </summary>
        public int TotalAchievements { get; set; }
    }
}

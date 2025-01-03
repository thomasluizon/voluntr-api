namespace Voluntr.Application.ViewModels
{
    public class VolunteerProfileViewModel
    {
        /// <summary>
        /// Informações do voluntário
        /// </summary>
        public VolunteerInformationViewModel Volunteer { get; set; }

        /// <summary>
        /// Conquistas gerais
        /// </summary>
        public List<AchievementForAchievementsPageViewModel> Achievements { get; set; }

        /// <summary>
        /// Quantidade de moedas acumuladas ao total
        /// </summary>
        public int TotalCoins { get; set; }

        /// <summary>
        /// Quantidade de tarefas completas ao total
        /// </summary>
        public int QuestsCompleted { get; set; }
    }

    public class VolunteerInformationViewModel
    {
        /// <summary>
        /// Apelido do voluntário
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Nome do voluntário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ano de registro do voluntário
        /// </summary>
        public string RegisterYear { get; set; }

        /// <summary>
        /// Foto de perfil do voluntário
        /// </summary>
        public string ImageUrl { get; set; }
    }
}

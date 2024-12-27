namespace Voluntr.Application.ViewModels
{
    public class OnboardingTaskViewModel
    {
        /// <summary>
        /// Nome da tarefa
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição da tarefa
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imagem da tarefa
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Caminho para ser redirecionado ao clicar no botão da tarefa
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// Flag que indica se a tarefa já foi concluida
        /// </summary>
        public bool Done { get; set; }
    }
}

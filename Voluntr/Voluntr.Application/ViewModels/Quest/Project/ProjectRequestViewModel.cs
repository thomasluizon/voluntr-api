namespace Voluntr.Application.ViewModels
{
    public class ProjectRequestViewModel
    {
        /// <summary>
        /// Código do projeto
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Nome do projeto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do projeto
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data final do projeto
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}

using Microsoft.AspNetCore.Http;

namespace Voluntr.Application.ViewModels
{
    public class SubmitQuestViewModel
    {
        /// <summary>
        /// Código da tarefa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Código do projeto
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Descrição do trabalho realizado pelo voluntário
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imagem anexada pelo voluntário
        /// </summary>
        public IFormFile Image { get; set; }
    }
}

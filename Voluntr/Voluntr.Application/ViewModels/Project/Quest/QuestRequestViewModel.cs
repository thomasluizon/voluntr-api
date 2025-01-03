namespace Voluntr.Application.ViewModels
{
    public class QuestRequestViewModel
    {
        /// <summary>
        /// Código da tarefa
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Código do projeto
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Nome da tarefa
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição da tarefa
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data limite da tarefa
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Recompensa da tarefa
        /// </summary>
        public int Reward { get; set; }
    }
}

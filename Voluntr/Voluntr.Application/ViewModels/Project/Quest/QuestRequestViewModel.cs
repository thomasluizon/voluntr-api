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

        /// <summary>
        /// Quantidade máxima de voluntários que podem se atribuir à tarefa
        /// </summary>
        public int MaxVolunteers { get; set; }

        /// <summary>
        /// Flag que indica se a tarefa precisa ser feita de forma remota
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// Endereço da tarefa
        /// </summary>
        public AddressViewModel Address { get; set; }
    }
}

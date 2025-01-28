namespace Voluntr.Application.ViewModels
{
    public class NgoRegisterViewModel
    {
        /// <summary>
        /// CNPJ da ONG
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Data de fundação da ONG
        /// </summary>
        public DateTime? FoundingDate { get; set; }

        /// <summary>
        /// Descrição da ONG
        /// </summary>
        public string Description { get; set; }
    }
}

namespace Voluntr.Application.ViewModels
{
    public class VolunteerRequestViewModel
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
        /// Sobrenome do voluntário
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Número de telefone do voluntário
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Endereço do voluntário
        /// </summary>
        public AddressViewModel Address { get; set; }
    }
}

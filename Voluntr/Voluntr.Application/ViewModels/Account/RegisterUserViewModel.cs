namespace Voluntr.Application.ViewModels
{
    public class RegisterUserViewModel
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Tipo do usuário (Voluntário, Ong ou Empresa)
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Número de telefone do usuário
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Endereço do usuário
        /// </summary>
        public AddressViewModel AddressViewModel { get; set; }

        /// <summary>
        /// Dados do voluntário
        /// </summary>
        public VolunteerRegisterViewModel VolunteerRegisterViewModel { get; set; }

        /// <summary>
        /// Dados da ONG
        /// </summary>
        public NgoRegisterViewModel NgoRegisterViewModel { get; set; }

        /// <summary>
        /// Dados da empresa
        /// </summary>
        public CompanyRegisterViewModel CompanyRegisterViewModel { get; set; }
    }
}

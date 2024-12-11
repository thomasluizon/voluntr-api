namespace Voluntr.Application.ViewModels
{
    public class AddressViewModel
    {
        /// <summary>
        /// Cep do endereço
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Logradouro do endereço
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Número do endereço
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Complemento do endereço
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Bairro do endereço
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// UF do endereço
        /// </summary>
        public string Uf { get; set; }

        /// <summary>
        /// Cidade do endereço
        /// </summary>
        public string City { get; set; }
    }
}

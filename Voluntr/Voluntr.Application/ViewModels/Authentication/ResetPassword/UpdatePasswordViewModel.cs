namespace Voluntr.Application.ViewModels
{
    public class UpdatePasswordViewModel
    {
        /// <summary>
        /// Senha atual do usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nova senha do usuário
        /// </summary>
        public string NewPassword { get; set; }
    }
}

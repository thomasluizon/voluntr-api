namespace Voluntr.Application.ViewModels
{
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Token de reset de senha
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Nova senha
        /// </summary>
        public string Password { get; set; }
    }
}

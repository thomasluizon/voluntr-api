namespace Voluntr.Domain.DataTransferObjects
{
    public class OAuthUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool EmailVerified { get; set; }
    }
}

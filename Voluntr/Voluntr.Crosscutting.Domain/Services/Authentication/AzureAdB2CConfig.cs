namespace Voluntr.Crosscutting.Domain.Services.Authentication
{
    public class AzureAdB2CConfig
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string CallbackPath { get; set; }
    }
}

namespace Voluntr.Application.ViewModels
{
    public class OAuthAuthenticationRequestViewModel
    {
        /// <summary>
        /// Token do OAuth Provider
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Provider do OAuth (Google)
        /// </summary>
        public string OAuthProviderName { get; set; }
    }
}

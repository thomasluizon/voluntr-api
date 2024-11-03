using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.Services
{
    public class OAuthService(
        HttpClient httpClient
    ) : IOAuthService
    {
        public async Task<GoogleOAuthUserDto> ValidateGoogleTokenAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var userInfoResponse = await httpClient.SendAsync(request);

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var payload = JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());

            return new GoogleOAuthUserDto
            {
                Email = payload.Value<string>("email"),
                Name = payload.Value<string>("name"),
                Picture = payload.Value<string>("picture")
            };
        }
    }
}

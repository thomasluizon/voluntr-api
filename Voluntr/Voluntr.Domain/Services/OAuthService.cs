using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Services
{
    public class OAuthService(
        HttpClient httpClient
    ) : IOAuthService
    {
        public async Task<OAuthUserDto> ValidateOAuthTokenAsync(string token, OAuthProvider OAuthProvider)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, OAuthProvider.UserInfoApiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var userInfoResponse = await httpClient.SendAsync(request);

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var payload = JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());

            return new OAuthUserDto
            {
                Email = payload.Value<string>(OAuthProvider.EmailProperty),
                Name = payload.Value<string>(OAuthProvider.NameProperty),
                Picture = payload.Value<string>(OAuthProvider.PictureProperty),
                EmailVerified = payload.Value<bool>(OAuthProvider.EmailVerifiedProperty),
            };
        }
    }
}

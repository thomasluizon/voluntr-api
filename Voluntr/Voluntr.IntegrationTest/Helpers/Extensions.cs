using System.Net.Http.Headers;
using System.Net.Http.Json;
using Voluntr.Application.ViewModels;

namespace Voluntr.IntegrationTest.Helpers
{
    public static class Extensions
    {
        public async static Task LoginAsync(this HttpClient client)
        {
            var loginRequest = new AuthenticationRequestViewModel
            {
                Email = "thomaslrgregorio@gmail.com",
                Password = "teste123"
            };

            var response = await client.PostAsJsonAsync("/auth/login", loginRequest);

            response.EnsureSuccessStatusCode();

            var loginResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponseViewModel>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse?.AccessToken);
        }
    }
}

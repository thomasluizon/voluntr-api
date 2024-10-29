using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Voluntr.Application.ViewModels;
using Voluntr.IntegrationTest.Helpers;

namespace Voluntr.IntegrationTest.Api.Controllers
{
    public class VolunteerControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client = factory.CreateClient();

        [Fact]
        public async Task GetVolunteers_ReturnsOkResponse_WithVolunteerList()
        {
            // Arrange: Login and get JWT Token
            await client.LoginAsync();

            // Act: GET request to the volunteers endpoint
            var response = await client.GetAsync("/volunteer");

            // Assert: Check if the status code is 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert: Check the response content (volunteers)
            var volunteers = await response.Content.ReadFromJsonAsync<List<VolunteerResponseViewModel>>();

            Assert.NotNull(volunteers);
            Assert.NotEmpty(volunteers);
        }

        [Fact]
        public async Task GetVolunteers_WhenNoVolunteersExist_ReturnsEmptyList()
        {
            // Arrange: Login and get JWT Token
            await client.LoginAsync();

            // Act: GET request to the volunteers endpoint
            var response = await client.GetAsync("/volunteer");

            // Assert: Check if the status code is 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert: Check the response content (volunteers)
            var volunteers = await response.Content.ReadFromJsonAsync<List<VolunteerResponseViewModel>>();

            Assert.NotNull(volunteers);
            Assert.Empty(volunteers);
        }

        [Fact]
        public async Task GetVolunteers_UnauthorizedAccess_ReturnsUnauthorizedResponse()
        {
            // Act: Try to access the volunteers endpoint without logging in
            var response = await client.GetAsync("/volunteer");

            // Assert: Check if the status code is 401 (Unauthorized)
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

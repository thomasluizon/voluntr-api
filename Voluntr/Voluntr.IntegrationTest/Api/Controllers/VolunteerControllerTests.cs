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
            // Arrange: Fazer login e obter o token JWT
            await client.LoginAsync();

            // Act: Fazer uma requisição GET para o endpoint de voluntários
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert: Verificar o conteúdo da resposta (voluntários)
            var volunteers = await response.Content.ReadFromJsonAsync<List<VolunteerResponseViewModel>>();

            Assert.NotNull(volunteers);
            Assert.NotEmpty(volunteers);  // Verificar que a lista não está vazia
        }

        [Fact]
        public async Task GetVolunteers_WhenNoVolunteersExist_ReturnsEmptyList()
        {
            // Arrange: Fazer login e obter o token JWT
            await client.LoginAsync();

            // Act: Fazer uma requisição GET para o endpoint de voluntários
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert: Verificar o conteúdo da resposta (voluntários)
            var volunteers = await response.Content.ReadFromJsonAsync<List<VolunteerResponseViewModel>>();

            Assert.NotNull(volunteers);
            Assert.Empty(volunteers);  // Espera uma lista vazia quando não há voluntários
        }

        [Fact]
        public async Task GetVolunteers_UnauthorizedAccess_ReturnsUnauthorizedResponse()
        {
            // Act: Tentar acessar o endpoint de voluntários sem autenticação
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 401 (Unauthorized)
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text.Json;
using Voluntr.Application.ViewModels;

namespace Voluntr.IntegrationTest.Api.Controllers
{
    public class VolunteerControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client = factory.CreateClient();

        [Fact]
        public async Task GetVolunteers_ReturnsOkResponse_WithVolunteerList()
        {
            // Act: Fazer uma requisição GET para o endpoint de voluntários
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert: Verificar o conteúdo da resposta (voluntários)
            var responseString = await response.Content.ReadAsStringAsync();
            var volunteers = JsonSerializer.Deserialize<List<VolunteerResponseViewModel>>(responseString);

            Assert.NotNull(volunteers);
            Assert.NotEmpty(volunteers);  // Verificar que a lista não está vazia
        }

        [Fact]
        public async Task GetVolunteers_WhenNoVolunteersExist_ReturnsEmptyList()
        {
            // Act: Fazer uma requisição GET para o endpoint de voluntários
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 200 (OK)
            var responseString = await response.Content.ReadAsStringAsync();
            var volunteers = JsonSerializer.Deserialize<List<VolunteerResponseViewModel>>(responseString);

            Assert.NotNull(volunteers);
            Assert.Empty(volunteers);  // Espera uma lista vazia quando não há voluntários
        }

        [Fact]
        public async Task GetVolunteers_UnauthorizedAccess_ReturnsUnauthorizedResponse()
        {
            // Arrange: Remover o cabeçalho de autenticação para simular acesso não autorizado
            client.DefaultRequestHeaders.Clear();

            // Act: Tentar acessar o endpoint de voluntários sem autenticação
            var response = await client.GetAsync("/volunteer");

            // Assert: Verificar se o status retornado é 401 (Unauthorized)
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

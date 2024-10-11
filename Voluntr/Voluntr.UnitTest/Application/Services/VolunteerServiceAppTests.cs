using AutoMapper;
using Moq;
using Voluntr.Application.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Queries;

namespace Voluntr.Tests.Application.Services
{
    public class VolunteerServiceAppTests
    {
        private readonly static Mock<IMediatorHandler> mediatorMock = new();
        private readonly static Mock<IMapper> mapperMock = new();
        private readonly static VolunteerServiceApp volunteerService = new(mediatorMock.Object, mapperMock.Object);

        [Fact]
        public async Task GetVolunteers_ReturnsListOfVolunteers()
        {
            // Arrange: Configurar os dados simulados para a resposta da query
            var mockVolunteerDtos = new List<VolunteerDto>
            {
                new() { Name = "John Doe" },
                new() { Name = "Jane Doe" }
            };

            // Simular o retorno do mediator quando a query GetVolunteersQuery for enviada
            mediatorMock
                .Setup(m => m.SendQuery(It.IsAny<GetVolunteersQuery>()))
                .ReturnsAsync(mockVolunteerDtos);

            // Simular o mapeamento usando o IMapper de VolunteerDto para VolunteerResponseViewModel
            var mockVolunteerResponseViewModels = mockVolunteerDtos
                .Select(v => new VolunteerResponseViewModel { Name = v.Name })
                .ToList();

            mapperMock
                .Setup(m => m.Map<List<VolunteerResponseViewModel>>(It.IsAny<List<VolunteerDto>>()))
                .Returns(mockVolunteerResponseViewModels);

            // Act: Chamar o método a ser testado
            var result = await volunteerService.GetVolunteers();

            // Assert: Verificar se o resultado é o esperado
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].Name);
        }

        [Fact]
        public async Task GetVolunteers_WhenNoVolunteers_ReturnsEmptyList()
        {
            // Arrange: Configurar o mediator para retornar uma lista vazia de VolunteerDto
            mediatorMock
                .Setup(m => m.SendQuery(It.IsAny<GetVolunteersQuery>()))
                .ReturnsAsync([]);

            // Simular o mapeamento para uma lista vazia de VolunteerResponseViewModel
            mapperMock
                .Setup(m => m.Map<List<VolunteerResponseViewModel>>(It.IsAny<List<VolunteerDto>>()))
                .Returns([]);

            // Act
            var result = await volunteerService.GetVolunteers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

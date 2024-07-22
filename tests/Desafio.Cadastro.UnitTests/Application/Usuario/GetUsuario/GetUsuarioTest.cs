using Desafio.Cadastro.Application.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.GetUsuario
{
    [Collection(nameof(GetUsuarioTestFixture))]
    public class GetUsuarioTest
    {
        private readonly GetUsuarioTestFixture _fixture;

        public GetUsuarioTest(GetUsuarioTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(GetUsuario))]
        [Trait("Application", "GetUsuario - Use Cases")]
        public async Task GetUsuario()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var exampleUsuario = _fixture.GetExampleUsuario();
            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleUsuario);
            var input = new UseCase.GetUsuarioInput(exampleUsuario.Id);
            var useCase = new UseCase.GetUsuario(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);
            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

            output.Should().NotBeNull();
            output.Name.Should().Be(exampleUsuario.Name);            
            output.Id.Should().Be(exampleUsuario.Id);            
        }

        [Fact(DisplayName = nameof(NotFoundExceptionWhenUsuarioDoesntExist))]
        [Trait("Application", "GetUsuario - Use Cases")]
        public async Task NotFoundExceptionWhenUsuarioDoesntExist()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var exampleGuid = Guid.NewGuid();
            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(
                new NotFoundException($"Usuario '{exampleGuid}' not found.")
            );
            var input = new UseCase.GetUsuarioInput(exampleGuid);
            var useCase = new UseCase.GetUsuario(repositoryMock.Object);

            var task = async () =>
                await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();
            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}

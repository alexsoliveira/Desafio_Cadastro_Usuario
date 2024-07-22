using Desafio.Cadastro.Application.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = Desafio.Cadastro.Application.UseCases.Usuario.DeleteUsuario;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.DeleteUsuario
{
    [Collection(nameof(DeleteUsuarioTestFixture))]
    public class DeleteUsuarioTest
    {
        private readonly DeleteUsuarioTestFixture _fixture;

        public DeleteUsuarioTest(DeleteUsuarioTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteUsuario))]
        [Trait("Application", "DeleteUsuario - Use Cases")]
        public async Task DeleteUsuario()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var UsuarioExample = _fixture.GetExampleUsuario();
            repositoryMock.Setup(x => x.Get(
                UsuarioExample.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(UsuarioExample);
            var input = new UseCase.DeleteUsuarioInput(UsuarioExample.Id);
            var useCase = new UseCase.DeleteUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object);

            await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(
                UsuarioExample.Id,
                It.IsAny<CancellationToken>()
            ), Times.Once);
            repositoryMock.Verify(x => x.Delete(
                UsuarioExample,
                It.IsAny<CancellationToken>()
            ), Times.Once);
            unitOfWorkMock.Verify(
                x => x.Commit(It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact(DisplayName = nameof(ThrowWhenUsuarioNotFound))]
        [Trait("Application", "DeleteUsuario - Use Cases")]
        public async Task ThrowWhenUsuarioNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exampleGuid = Guid.NewGuid();
            repositoryMock.Setup(x => x.Get(
                exampleGuid,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(
                new NotFoundException($"Usuario '{exampleGuid}' not found")
            );
            var input = new UseCase.DeleteUsuarioInput(exampleGuid);
            var useCase = new UseCase.DeleteUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object);

            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                exampleGuid,
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}

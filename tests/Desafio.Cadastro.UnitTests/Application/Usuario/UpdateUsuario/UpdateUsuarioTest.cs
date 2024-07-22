using Desafio.Cadastro.Application.Exceptions;
using Desafio.Cadastro.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario;
using Desafio.Cadastro.Application.UseCases.Usuario.Common;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.UpdateUsuario
{
    [Collection(nameof(UpdateUsuarioTestFixture))]
    public class UpdateUsuarioTest
    {
        private readonly UpdateUsuarioTestFixture _fixture;

        public UpdateUsuarioTest(UpdateUsuarioTestFixture fixture)
            => _fixture = fixture;

        [Theory(DisplayName = nameof(UpdateUsuario))]
        [Trait("Application", "UpdateUsuario - Use Cases")]
        [MemberData(
            nameof(UpdateUsuarioTestDataGenerator.GetUsuariosToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateUsuarioTestDataGenerator)
        )]
        public async Task UpdateUsuario(
            DomainEntity.Usuario exampleUsuario,
            UpdateUsuarioInput input)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleUsuario);
            var useCase = new UseCase.UpdateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            UsuarioModelOutput output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);                        
            repositoryMock.Verify(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleUsuario,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Theory(DisplayName = nameof(UpdateUsuarioWithoutProvidingIsActive))]
        [Trait("Application", "UpdateUsuario - Use Cases")]
        [MemberData(
            nameof(UpdateUsuarioTestDataGenerator.GetUsuariosToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateUsuarioTestDataGenerator)
        )]
        public async Task UpdateUsuarioWithoutProvidingIsActive(
            DomainEntity.Usuario exampleUsuario,
            UpdateUsuarioInput exampleInput)
        {
            var input = new UpdateUsuarioInput(
                exampleInput.Id,
                exampleInput.Name               
            );
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleUsuario);
            var useCase = new UseCase.UpdateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            UsuarioModelOutput output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);                        
            repositoryMock.Verify(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleUsuario,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Theory(DisplayName = nameof(UpdateUsuarioOnlyName))]
        [Trait("Application", "UpdateUsuario - Use Cases")]
        [MemberData(
            nameof(UpdateUsuarioTestDataGenerator.GetUsuariosToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateUsuarioTestDataGenerator)
        )]
        public async Task UpdateUsuarioOnlyName(
            DomainEntity.Usuario exampleUsuario,
            UpdateUsuarioInput exampleInput)
        {
            var input = new UpdateUsuarioInput(
                exampleInput.Id,
                exampleInput.Name
            );
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleUsuario);
            var useCase = new UseCase.UpdateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            UsuarioModelOutput output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);            
            repositoryMock.Verify(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleUsuario,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact(DisplayName = nameof(ThrowWhenUsuarioNotFound))]
        [Trait("Application", "UpdateUsuario - Use Cases")]

        public async Task ThrowWhenUsuarioNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var input = _fixture.GetValidInput();
            repositoryMock.Setup(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Usuario '{input.Id}' not found."));
            var useCase = new UseCase.UpdateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);

        }

        [Theory(DisplayName = nameof(ThrowWhenCantUpdateUsuario))]
        [Trait("Application", "UpdateUsuario - Use Cases")]
        [MemberData(
            nameof(UpdateUsuarioTestDataGenerator.GetInvalidInputs),
            parameters: 12,
            MemberType = typeof(UpdateUsuarioTestDataGenerator)
        )]
        public async Task ThrowWhenCantUpdateUsuario(
            UpdateUsuarioInput input,
            string expectedExceptionMessage
        )
        {
            var exampleUsuario = _fixture.GetExampleUsuario();
            input.Id = exampleUsuario.Id;
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleUsuario);
            var useCase = new UseCase.UpdateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>()
                .WithMessage(expectedExceptionMessage);

            repositoryMock.Verify(x => x.Get(
                exampleUsuario.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);
        }
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario;
using Desafio.Cadastro.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using UseCase = Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.CreateUsuario
{
    [Collection(nameof(CreateUsuarioTestFixture))]
    public class CreateUsuarioTest
    {
        private readonly CreateUsuarioTestFixture _fixture;

        public CreateUsuarioTest(
            CreateUsuarioTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateUsuario))]
        [Trait("Application", "CreateUsuario - Use Cases")]
        public async void CreateUsuario()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCase.CreateUsuario(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            var input = _fixture.GetInput();
            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<DomainEntity.Usuario>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
            unitOfWorkMock.Verify(
                uow => uow.Commit(
                    It.IsAny<CancellationToken>()
                )
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Id.Should().NotBeEmpty();
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateUsuario))]
        [Trait("Application", "CreateUsuario - Use Cases")]
        [MemberData(
            nameof(CreateUsuarioTestDataGenerator.GetInvalidInputs),
            parameters: 6,
            MemberType = typeof(CreateUsuarioTestDataGenerator)
        )]
        public async void ThrowWhenCantInstantiateUsuario(
            CreateUsuarioInput input,
            string exceptionMessage
        )
        {
            var useCase = new UseCase.CreateUsuario(
                _fixture.GetRepositoryMock().Object,
                _fixture.GetUnitOfWorkMock().Object
            );

            Func<Task> task =
                async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);

        }
    }
}

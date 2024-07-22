using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using UseCase = Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.ListUsuarios
{
    [Collection(nameof(ListUsuariosTestFixture))]
    public class ListUsuariosTest
    {
        private readonly ListUsuariosTestFixture _fixture;

        public ListUsuariosTest(ListUsuariosTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(List))]
        [Trait("Application", "ListUsuarios - Use Cases")]
        public async Task List()
        {
            var usuariosExampleList = _fixture.GetExampleUsuariosList();
            var repositoryMock = _fixture.GetRepositoryMock();
            var input = _fixture.GetExampleInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Usuario>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<DomainEntity.Usuario>)usuariosExampleList,
                total: new Random().Next(50, 200)
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListUsuarios(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<UsuarioModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryUsuario = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);

                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryUsuario!.Name);                
            });
            repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("Application", "ListUsuarios - Use Cases")]
        public async Task ListOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var input = _fixture.GetExampleInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Usuario>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: new List<DomainEntity.Usuario>().AsReadOnly(),
                total: 0
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListUsuarios(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);

            repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
        [Trait("Application", "ListUsuarios - Use Cases")]
        [MemberData(
            nameof(ListUsuariosTestDataGenerator.GetInputWithoutAllParameters),
            parameters: 12,
            MemberType = typeof(ListUsuariosTestDataGenerator))]
        public async Task ListInputWithoutAllParameters(ListUsuariosInput input)
        {
            var usuariosExampleList = _fixture.GetExampleUsuariosList();
            var repositoryMock = _fixture.GetRepositoryMock();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Usuario>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<DomainEntity.Usuario>)usuariosExampleList,
                total: new Random().Next(50, 200)
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListUsuarios(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<UsuarioModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryUsuario = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);

                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryUsuario!.Name);                
            });
            repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}

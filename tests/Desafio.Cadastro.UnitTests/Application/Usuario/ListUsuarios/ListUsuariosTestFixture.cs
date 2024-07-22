using Bogus;
using Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;
using Desafio.Cadastro.UnitTests.Application.Usuario.Common;
using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.ListUsuarios
{    
    [CollectionDefinition(nameof(ListUsuariosTestFixture))]
    public class ListUsuariosTestFixtureCollection
    : ICollectionFixture<ListUsuariosTestFixture>
    { }

    public class ListUsuariosTestFixture
        : UsuarioUseCasesBaseFixture
    {
        public List<DomainEntity.Usuario> GetExampleUsuariosList(int length = 10)
        {
            var list = new List<DomainEntity.Usuario>();
            for (var i = 0; i < length; i++)
                list.Add(GetExampleUsuario());
            return list;
        }

        public ListUsuariosInput GetExampleInput()
        {
            var random = new Random();
            return new ListUsuariosInput(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Commerce.ProductName(),
                dir: random.Next(0, 10) > 5 ?
                    SearchOrder.Asc : SearchOrder.Desc
            );
        }
    }
}

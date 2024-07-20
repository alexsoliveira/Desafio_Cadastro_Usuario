using Bogus;
using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;

namespace Desafio.Cadastro.UnitTests.Domain.Entity.Usuario
{
    [CollectionDefinition(nameof(UsuarioTestFixture))]
    public class UsuarioTextFixtureCollection
    : ICollectionFixture<UsuarioTestFixture>
    { }

    public class UsuarioTestFixture
    {
        public Faker Faker { get; set; }

        public UsuarioTestFixture()
            => Faker = new Faker("pt_BR");

        public string ObterUsuarioNomeValido()
        {
            var usuarioNome = "";

            while (usuarioNome.Length < 3)
                usuarioNome = Faker.Commerce.Categories(1)[0];

            if (usuarioNome.Length > 255)
                usuarioNome = usuarioNome[..255];

            return usuarioNome;
        }

        public DomainEntity.Usuario ObterUsuarioValido()
            => new(
                ObterUsuarioNomeValido()
            );
    }
}

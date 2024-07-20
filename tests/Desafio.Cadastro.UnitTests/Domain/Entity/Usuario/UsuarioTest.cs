using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using FluentAssertions;

namespace Desafio.Cadastro.UnitTests.Domain.Entity.Usuario
{
    [Collection(nameof(UsuarioTestFixture))]
    public class UsuarioTest
    {        
        private readonly UsuarioTestFixture _usuarioTestFixture;

        public UsuarioTest(UsuarioTestFixture usuarioTestFixture)
            => _usuarioTestFixture = usuarioTestFixture;        

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Usuario - Aggregates")]
        public void Instantiate()
        {
            var usuarioValido = _usuarioTestFixture.ObterUsuarioValido();

            var usuario = new DomainEntity.Usuario(usuarioValido.Nome);

            usuario.Should().NotBeNull();
            usuario.Nome.Should().Be(usuarioValido.Nome);
            usuario.Id.Should().NotBeEmpty();
        }
    }
}

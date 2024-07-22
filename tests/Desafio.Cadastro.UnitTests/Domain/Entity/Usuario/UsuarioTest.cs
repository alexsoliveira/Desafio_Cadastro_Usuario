using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using FluentAssertions;
using Desafio.Cadastro.Domain.Exceptions;

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
            var usuarioValido = _usuarioTestFixture.GetValidUsuario();

            var usuario = new DomainEntity.Usuario(usuarioValido.Name);

            usuario.Should().NotBeNull();
            usuario.Name.Should().Be(usuarioValido.Name);
            usuario.Id.Should().NotBeEmpty();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Usuario - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {            
            Action action =
                () => new DomainEntity.Usuario(name!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");            
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Usuario - Aggregates")]
        [MemberData(nameof(GetNamesWithLessThan3Caracters), parameters: 10)]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {            
            Action action =
                () => new DomainEntity.Usuario(invalidName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at leats 3 characters long");
        }

        public static IEnumerable<object[]> GetNamesWithLessThan3Caracters(int numberOfTests = 6)
        {
            var fixture = new UsuarioTestFixture();

            for (int i = 0; i < numberOfTests; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] {
                fixture.GetValidUsuarioName()[ ..(isOdd ? 1 : 2)]
            };
            }
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan15Characters))]
        [Trait("Domain", "Usuario - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan15Characters()
        {
            var validUsuario = _usuarioTestFixture.GetValidUsuario();
            var invalidName = string.Join(null, Enumerable.Range(1, 16).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Usuario(invalidName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 15 characters long");            
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Usuario - Aggregates")]
        public void UpdateOnlyName()
        {
            var usuarioValido = _usuarioTestFixture.GetValidUsuario();

            var newName = _usuarioTestFixture.GetValidUsuarioName();

            usuarioValido.Update(newName);

            usuarioValido.Name.Should().Be(newName);
        }
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario;
using Desafio.Cadastro.UnitTests.Application.Usuario.Common;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.CreateUsuario
{
    [CollectionDefinition(nameof(CreateUsuarioTestFixture))]
    public class CreateUsuarioTestFixtureCollection
        :ICollectionFixture<CreateUsuarioTestFixture>
    { }

    public class CreateUsuarioTestFixture : UsuarioUseCasesBaseFixture
    {
        public CreateUsuarioInput GetInput()
            => new(
                GetValidUsuarioName()
            );

        public CreateUsuarioInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }

        public CreateUsuarioInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetInput();
            var tooLongNameForUsuario = Faker.Name.FirstName();
            while (tooLongNameForUsuario.Length <= 15)
                tooLongNameForUsuario = $"{tooLongNameForUsuario} {Faker.Name.FirstName()}";
            invalidInputTooLongName.Name = tooLongNameForUsuario;
            return invalidInputTooLongName;
        }
    }
}

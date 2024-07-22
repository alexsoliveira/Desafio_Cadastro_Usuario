using Bogus;
using Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario;
using Desafio.Cadastro.UnitTests.Application.Usuario.Common;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.UpdateUsuario
{   
    [CollectionDefinition(nameof(UpdateUsuarioTestFixture))]
    public class UpdateUsuariotestFixtureCollection
    : ICollectionFixture<UpdateUsuarioTestFixture>
    { }

    public class UpdateUsuarioTestFixture
        : UsuarioUseCasesBaseFixture
    {
        public UpdateUsuarioInput GetValidInput(Guid? id = null)
            => new(
                    id ?? Guid.NewGuid(),
                    GetValidUsuarioName()                                        
                );

        public UpdateUsuarioInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetValidInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }

        public UpdateUsuarioInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetValidInput();
            var tooLongNameForUsuario = Faker.Name.FindName();
            while (tooLongNameForUsuario.Length <= 15)
                tooLongNameForUsuario = $"{tooLongNameForUsuario} {Faker.Name.FindName()}";
            invalidInputTooLongName.Name = tooLongNameForUsuario;
            return invalidInputTooLongName;
        }        
    }
}

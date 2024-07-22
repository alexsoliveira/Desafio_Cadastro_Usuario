using Desafio.Cadastro.UnitTests.Application.Usuario.Common;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.DeleteUsuario
{
    [CollectionDefinition(nameof(DeleteUsuarioTestFixture))]
    public class DeleteUsuarioTestFixtureCollection
    : ICollectionFixture<DeleteUsuarioTestFixture>
    { }

    public class DeleteUsuarioTestFixture : UsuarioUseCasesBaseFixture
    { }
}

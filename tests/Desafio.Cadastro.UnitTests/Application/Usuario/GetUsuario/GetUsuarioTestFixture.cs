using Desafio.Cadastro.UnitTests.Application.Usuario.Common;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.GetUsuario
{    
    [CollectionDefinition(nameof(GetUsuarioTestFixture))]
    public class GetUsuarioTestFixtureCollection
    : ICollectionFixture<GetUsuarioTestFixture>
    { }

    public class GetUsuarioTestFixture : UsuarioUseCasesBaseFixture
    { }
}

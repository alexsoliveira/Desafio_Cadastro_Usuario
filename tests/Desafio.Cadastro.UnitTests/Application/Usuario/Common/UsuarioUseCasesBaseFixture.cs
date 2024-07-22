using Desafio.Cadastro.Domain.Repository;
using Desafio.Cadastro.UnitTests.Common;
using DomainEntity = Desafio.Cadastro.Domain.Entity;
using Moq;
using Desafio.Cadastro.Application.Interfaces;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.Common
{
    public abstract class UsuarioUseCasesBaseFixture
        : BaseFixture
    {
        public Mock<IUsuarioRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
            => new();

        public string GetValidUsuarioName()
        {
            var usuarioNome = "";

            while (usuarioNome.Length < 3)
                usuarioNome = Faker.Name.FirstName();

            if (usuarioNome.Length > 15)
                usuarioNome = usuarioNome[..15];

            return usuarioNome;
        }

        public DomainEntity.Usuario GetExampleUsuario()
            => new(
                GetValidUsuarioName()
            );
    }
}

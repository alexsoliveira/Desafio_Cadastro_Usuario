using DomainEntity = Desafio.Cadastro.Domain.Entity;

namespace Desafio.Cadastro.Application.UseCases.Usuario.Common
{
    public class UsuarioModelOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UsuarioModelOutput(Guid id,
            string name)
        {
            Id = id;
            Name = name;
        }

        public static UsuarioModelOutput FromUsuario(DomainEntity.Usuario usuario)
            => new(
                usuario.Id,
                usuario.Name
            );
    }
}

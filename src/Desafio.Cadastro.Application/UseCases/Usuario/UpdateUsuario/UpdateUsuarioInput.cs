using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario
{
    public class UpdateUsuarioInput : IRequest<UsuarioModelOutput>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateUsuarioInput(
        Guid id,
        string name)
        {
            Id = id;
            Name = name;
        }
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario
{
    public class GetUsuarioInput : IRequest<UsuarioModelOutput>
    {
        public Guid Id { get; set; }
        public GetUsuarioInput(Guid id)
            => Id = id;
    }
}

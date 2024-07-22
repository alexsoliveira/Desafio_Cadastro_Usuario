using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario
{
    public interface IUpdateUsuario
        : IRequestHandler<UpdateUsuarioInput, UsuarioModelOutput>
    {
    }
}

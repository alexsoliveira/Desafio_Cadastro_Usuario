using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario
{
    public interface IGetUsuario
        :IRequestHandler<GetUsuarioInput, UsuarioModelOutput>
    { }
}

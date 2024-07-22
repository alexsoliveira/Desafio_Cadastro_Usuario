using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario
{
    public interface ICreateUsuario
        : IRequestHandler<CreateUsuarioInput, UsuarioModelOutput>
    { }
}

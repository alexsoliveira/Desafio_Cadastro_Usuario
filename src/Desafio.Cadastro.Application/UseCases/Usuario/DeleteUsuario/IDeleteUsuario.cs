using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.DeleteUsuario
{
    internal interface IDeleteUsuario
        : IRequestHandler<DeleteUsuarioInput>
    { }
}

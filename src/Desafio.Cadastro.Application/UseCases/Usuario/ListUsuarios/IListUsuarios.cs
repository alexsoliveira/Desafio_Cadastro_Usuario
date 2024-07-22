using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios
{
    internal interface IListUsuarios
        : IRequestHandler<ListUsuariosInput, ListUsuariosOutput>
    { }
}

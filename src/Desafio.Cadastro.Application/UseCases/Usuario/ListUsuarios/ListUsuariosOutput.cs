using Desafio.Cadastro.Application.Common;
using Desafio.Cadastro.Application.UseCases.Usuario.Common;

namespace Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios
{
    public class ListUsuariosOutput
    : PaginatedListOutput<UsuarioModelOutput>
    {
        public ListUsuariosOutput(
            int page,
            int perPage,
            int total,
            IReadOnlyList<UsuarioModelOutput> items)
            : base(page, perPage, total, items)
        {
        }
    }
}

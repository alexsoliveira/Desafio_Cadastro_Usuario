using Desafio.Cadastro.Application.Common;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios
{
    public class ListUsuariosInput
    : PaginatedListInput, IRequest<ListUsuariosOutput>
    {
        public ListUsuariosInput(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc)
            : base(page, perPage, search, sort, dir)
        { }

        public ListUsuariosInput()
            : base(1, 15, "", "", SearchOrder.Asc)
        { }
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Domain.Repository;

namespace Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios
{
    public class ListUsuarios : IListUsuarios
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ListUsuarios(IUsuarioRepository usuarioRepository)
            => _usuarioRepository = usuarioRepository;

        public async Task<ListUsuariosOutput> Handle(
            ListUsuariosInput request,
            CancellationToken cancellationToken)
        {
            var searchOutput = await _usuarioRepository.Search(
                new(
                    request.Page,
                    request.PerPage,
                    request.Search,
                    request.Sort,
                    request.Dir
                ),
                cancellationToken
            );

            return new ListUsuariosOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items
                .Select(UsuarioModelOutput.FromUsuario)
                .ToList()
            );
        }
    }
}

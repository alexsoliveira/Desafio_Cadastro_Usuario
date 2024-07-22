using Desafio.Cadastro.Application.Exceptions;
using Desafio.Cadastro.Domain.Entity;
using Desafio.Cadastro.Domain.Repository;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Cadastro.Infra.Data.EF.Repositories
{
    public class UsuarioRepository
        : IUsuarioRepository
    {
        private readonly UsuarioDbContext _context;
        private DbSet<Usuario> _usuarios
            => _context.Set<Usuario>();       

        public UsuarioRepository(UsuarioDbContext context)
            => _context = context;

        public async Task Insert(Usuario aggregate, CancellationToken cancellationToken)
            => await _usuarios.AddAsync(aggregate, cancellationToken);

        public async Task<Usuario> Get(Guid id, CancellationToken cancellationToken)
        {            
            var usuario = await _usuarios.AsNoTracking().FirstOrDefaultAsync(
                c => c.Id == id,
                cancellationToken
            );
            
            NotFoundException.ThrowIfNull(usuario, $"Usuario '{id}' not found.");
            return usuario!;
        }

        public Task Update(Usuario aggregate, CancellationToken _)
            => Task.FromResult(_usuarios.Update(aggregate));

        public Task Delete(Usuario aggregate, CancellationToken _)
            => Task.FromResult(_usuarios.Remove(aggregate));

        public async Task<SearchOutput<Usuario>> Search(
            SearchInput input,
            CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _usuarios.AsNoTracking();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);
            if (!String.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));
            var total = await query.CountAsync();
            var items = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync();
            return new(input.Page, input.PerPage, total, items);
        }

        private IQueryable<Usuario> AddOrderToQuery(
            IQueryable<Usuario> query,
            string orderProperty,
            SearchOrder order
        )
            => (orderProperty.ToLower(), order) switch
            {
                ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name),
                ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name),
                ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),                                
                _ => query.OrderBy(x => x.Name)
            };
    }
}

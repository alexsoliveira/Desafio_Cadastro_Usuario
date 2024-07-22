using Desafio.Cadastro.Application.Interfaces;

namespace Desafio.Cadastro.Infra.Data.EF
{
    public class UnitOfWork
        : IUnitOfWork
    {
        private readonly UsuarioDbContext _context;

        public UnitOfWork(UsuarioDbContext context)
         => _context = context;

        public Task Commit(CancellationToken cancellationToken)
         => _context.SaveChangesAsync(cancellationToken);

        public Task Rollback(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

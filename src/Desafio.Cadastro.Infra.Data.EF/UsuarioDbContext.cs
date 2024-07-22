using Desafio.Cadastro.Domain.Entity;
using Desafio.Cadastro.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Cadastro.Infra.Data.EF
{
    public class UsuarioDbContext :DbContext
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public UsuarioDbContext(
            DbContextOptions<UsuarioDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}

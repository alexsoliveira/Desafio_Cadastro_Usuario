using Desafio.Cadastro.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Cadastro.Infra.Data.EF.Configurations
{
    internal class UsuarioConfiguration
        : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(usuario => usuario.Id);
            builder.Property(usuario => usuario.Name)
                .HasMaxLength(15);            
        }
    }
}

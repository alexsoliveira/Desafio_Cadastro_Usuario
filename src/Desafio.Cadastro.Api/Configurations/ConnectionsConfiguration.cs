using Desafio.Cadastro.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Cadastro.Api.Configurations
{
    public static class ConnectionsConfiguration
    {
        public static IServiceCollection AddAppConections(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbConnection(configuration);
            return services;
        }

        private static IServiceCollection AddDbConnection(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection");
            services.AddDbContext<UsuarioDbContext>(
                options => options.UseNpgsql(
                    connectionString
                )
            );
            return services;
        }
    }
}

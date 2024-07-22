using Desafio.Cadastro.Application.Interfaces;
using Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario;
using Desafio.Cadastro.Domain.Repository;
using Desafio.Cadastro.Infra.Data.EF;
using Desafio.Cadastro.Infra.Data.EF.Repositories;

namespace Desafio.Cadastro.Api.Configurations
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection AddUseCases(
            this IServiceCollection services
        )
        {
            services.AddMediatR(cfg 
                => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUsuario)));
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services
        )
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}

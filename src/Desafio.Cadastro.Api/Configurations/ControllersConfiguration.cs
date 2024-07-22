using Desafio.Cadastro.Api.Filters;
using Microsoft.OpenApi.Models;

namespace Desafio.Cadastro.Api.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddAndConfigureControllers(
            this IServiceCollection services
        )
        {
            services.AddControllers(options 
                => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            );            
            services.AddDocumentation();
            return services;
        }

        private static IServiceCollection AddDocumentation(
            this IServiceCollection services
        )
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Cadastro Usuario", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }

        public static WebApplication UseDocumentation(
            this WebApplication app
        )
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}

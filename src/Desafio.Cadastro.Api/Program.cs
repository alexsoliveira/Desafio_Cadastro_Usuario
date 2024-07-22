using Desafio.Cadastro.Api.Configurations;

namespace Desafio.Cadastro.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            
            builder.Services
                .AddAppConections(builder.Configuration)
                .AddUseCases()
                .AddAndConfigureControllers()
                .AddCors(p => p.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                }));            

            var app = builder.Build();            
            app.UseDocumentation();
            app.UseHttpsRedirection();
            app.UseCors("CORS");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

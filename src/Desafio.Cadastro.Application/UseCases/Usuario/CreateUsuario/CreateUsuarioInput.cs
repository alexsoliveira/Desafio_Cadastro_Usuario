using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario
{
    public class CreateUsuarioInput : IRequest<UsuarioModelOutput>
    {
        public string Name { get; set; }

        public CreateUsuarioInput(
        string name)
        {
            Name = name;            
        }
    }
}

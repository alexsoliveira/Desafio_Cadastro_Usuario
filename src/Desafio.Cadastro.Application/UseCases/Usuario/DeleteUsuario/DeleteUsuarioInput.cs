using MediatR;

namespace Desafio.Cadastro.Application.UseCases.Usuario.DeleteUsuario
{
    public class DeleteUsuarioInput : IRequest
    {
        public Guid Id { get; set; }
        public DeleteUsuarioInput(Guid id)
            => Id = id;
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Domain.Repository;

namespace Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario
{
    public class GetUsuario : IGetUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuario(IUsuarioRepository usuarioRepository)
            => _usuarioRepository = usuarioRepository;

        public async Task<UsuarioModelOutput> Handle(
            GetUsuarioInput input, 
            CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.Get(input.Id, cancellationToken);
            return UsuarioModelOutput.FromUsuario(usuario);
        }
    }
}

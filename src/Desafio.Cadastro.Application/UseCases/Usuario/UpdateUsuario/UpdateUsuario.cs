using Desafio.Cadastro.Application.Interfaces;
using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Domain.Repository;

namespace Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario
{
    public class UpdateUsuario : IUpdateUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUsuario(
            IUsuarioRepository usuarioRepository,
            IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioModelOutput> Handle(UpdateUsuarioInput request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.Get(request.Id, cancellationToken);
            usuario.Update(request.Name);
            
            await _usuarioRepository.Update(usuario, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            return UsuarioModelOutput.FromUsuario(usuario);
        }
    }
}

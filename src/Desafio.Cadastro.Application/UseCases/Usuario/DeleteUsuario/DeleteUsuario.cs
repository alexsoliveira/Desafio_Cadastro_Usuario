using Desafio.Cadastro.Application.Interfaces;
using Desafio.Cadastro.Domain.Repository;

namespace Desafio.Cadastro.Application.UseCases.Usuario.DeleteUsuario
{
    public class DeleteUsuario : IDeleteUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsuario(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
            => (_usuarioRepository, _unitOfWork) = (usuarioRepository, unitOfWork);

        public async Task Handle(DeleteUsuarioInput request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.Get(request.Id, cancellationToken);

            await _usuarioRepository.Delete(usuario, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
        }
    }
}

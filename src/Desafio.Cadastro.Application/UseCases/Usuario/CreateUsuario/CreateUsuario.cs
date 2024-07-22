using Desafio.Cadastro.Application.Interfaces;
using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Domain.Repository;
using DomainEntity = Desafio.Cadastro.Domain.Entity;

namespace Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario
{   
    public class CreateUsuario : ICreateUsuario
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateUsuario(
            IUsuarioRepository usuarioRepository, 
            IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioModelOutput> Handle(
            CreateUsuarioInput input, 
            CancellationToken cancellationToken)
        {
            var usuario = new DomainEntity.Usuario(
                input.Name
            );

            await _usuarioRepository.Insert(usuario, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return UsuarioModelOutput.FromUsuario(usuario);
        }
    }
}

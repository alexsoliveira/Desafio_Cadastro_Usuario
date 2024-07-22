using Desafio.Cadastro.Domain.Entity;
using Desafio.Cadastro.Domain.SeedWork;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;

namespace Desafio.Cadastro.Domain.Repository
{
    public interface IUsuarioRepository
    : IGenericRepository<Usuario>,
        ISearchableRepository<Usuario>
    { }
}

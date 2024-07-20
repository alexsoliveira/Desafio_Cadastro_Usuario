using Desafio.Cadastro.Domain.SeedWork;

namespace Desafio.Cadastro.Domain.Entity
{
    public class Usuario : AggregateRoot
    {
        public string Nome { get; private set; }


        public Usuario(string nome)
        {
            Nome = nome;
        }                
    }
}

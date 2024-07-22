using Desafio.Cadastro.Domain.SeedWork;
using Desafio.Cadastro.Domain.Validation;

namespace Desafio.Cadastro.Domain.Entity
{
    public class Usuario : AggregateRoot
    {
        public string Name { get; private set; }


        public Usuario(string name)
        {
            Name = name;

            Validate();
        }

        public void Update(string name)
        {
            Name = name;            

            Validate();
        }

        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLength(Name, 3, nameof(Name));
            DomainValidation.MaxLength(Name, 15, nameof(Name));
        }
    }
}

using Desafio.Cadastro.Domain.Exceptions;

namespace Desafio.Cadastro.Domain.Validation
{
    public class DomainValidation
    {      
        public static void NotNullOrEmpty(string? target, string fieldName)
        {
            if (String.IsNullOrWhiteSpace(target))
                throw new EntityValidationException(
                    $"{fieldName} should not be empty or null");
        }

        public static void MinLength(string target, int minLength, string fieldName)
        {
            if (target.Length < minLength)
                throw new EntityValidationException(
                    $"{fieldName} should be at leats {minLength} characters long");
        }

        public static void MaxLength(string target, int maxLength, string fieldName)
        {
            if (target.Length > maxLength)
                throw new EntityValidationException(
                    $"{fieldName} should be less or equal {maxLength} characters long");
        }
    }
}

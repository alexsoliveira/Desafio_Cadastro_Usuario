using Bogus;
using Desafio.Cadastro.Domain.Exceptions;
using Desafio.Cadastro.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(NotNullOrEmptyOK))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOK()
        {
            var target = Faker.Name.FirstName();
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(target, fieldName);
            action.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action
                .Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be empty or null");
        }

        [Theory(DisplayName = nameof(MinLengthOK))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanTheMin), parameters: 10)]
        public void MinLengthOK(string target, int minLength)
        {
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action
                .Should()
                .NotThrow();
        }

        public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numbersOfTests = 5)
        {
            yield return new object[] { "12345", 5 };

            var faker = new Faker();

            for (int i = 0; i < (numbersOfTests - 1); i++)
            {
                var example = faker.Name.FirstName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanTheMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action
                .Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at leats {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numbersOfTests = 5)
        {
            yield return new object[] { "12345", 10 };

            var faker = new Faker();

            for (int i = 0; i < (numbersOfTests - 1); i++)
            {
                var example = faker.Name.FirstName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            }
        }
        // tamanho maximo
        [Theory(DisplayName = nameof(MaxLengthOK))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesLessThanTheMax), parameters: 10)]
        public void MaxLengthOK(string target, int minLength)
        {
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, minLength, fieldName);

            action
                .Should()
                .NotThrow();
        }

        public static IEnumerable<object[]> GetValuesLessThanTheMax(int numbersOfTests = 5)
        {
            yield return new object[] { "12345", 5 };

            var faker = new Faker();

            for (int i = 0; i < (numbersOfTests - 1); i++)
            {
                var example = faker.Name.FirstName();
                var maxLength = example.Length + (new Random()).Next(0, 5);
                yield return new object[] { example, maxLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanTheMax), parameters: 10)]
        public void MaxLengthThrowWhenGreater(string target, int maxLength)
        {
            var fieldName = Faker.Name.FirstName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action
                .Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanTheMax(int numbersOfTests = 5)
        {
            yield return new object[] { "123456", 5 };

            var faker = new Faker();

            for (int i = 0; i < (numbersOfTests - 1); i++)
            {
                var example = faker.Name.FirstName();
                var maxLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, maxLength };
            }
        }
    }
}

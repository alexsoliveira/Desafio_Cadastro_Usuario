using Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario;
using FluentAssertions;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.GetUsuario
{
    [Collection(nameof(GetUsuarioTestFixture))]
    public class GetUsuarioInputValidatorTest
    {
        private readonly GetUsuarioTestFixture _fixture;

        public GetUsuarioInputValidatorTest(GetUsuarioTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(ValidationOk))]
        [Trait("Application", "GetUsuarioInputValidation - UseCases")]
        public void ValidationOk()
        {
            var validIput = new GetUsuarioInput(Guid.NewGuid());
            var validator = new GetUsuarioInputValidator();

            var validationResult = validator.Validate(validIput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
        [Trait("Application", "GetUsuarioInputValidation - UseCases")]
        public void InvalidWhenEmptyGuidId()
        {
            var validIput = new GetUsuarioInput(Guid.Empty);
            var validator = new GetUsuarioInputValidator();

            var validationResult = validator.Validate(validIput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage
                .Should().Be("'Id' must not be empty.");
        }
    }
}

using Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario;
using FluentAssertions;
using Xunit;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.UpdateUsuario
{
    [Collection(nameof(UpdateUsuarioTestFixture))]
    public class UpdateUsuarioInputValidatorTest
    {
        private readonly UpdateUsuarioTestFixture _fixture;

        public UpdateUsuarioInputValidatorTest(UpdateUsuarioTestFixture fixture)
         => _fixture = fixture;

        [Fact(DisplayName = nameof(DontValidateWhenEmptyGuid))]
        [Trait("Application", "UpdateUsuarioInputValidator - Use Cases")]
        public void DontValidateWhenEmptyGuid()
        {
            var input = _fixture.GetValidInput(Guid.Empty);
            var validator = new UpdateUsuarioInputValidator();

            var validateResult = validator.Validate(input);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeFalse();
            validateResult.Errors.Should().HaveCount(1);
            validateResult.Errors[0].ErrorMessage
                .Should().Be("'Id' must not be empty.");
        }

        [Fact(DisplayName = nameof(ValidateWhenValid))]
        [Trait("Application", "UpdateUsuarioInputValidator - Use Cases")]
        public void ValidateWhenValid()
        {
            var input = _fixture.GetValidInput();
            var validator = new UpdateUsuarioInputValidator();

            var validateResult = validator.Validate(input);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeTrue();
            validateResult.Errors.Should().HaveCount(0);
        }
    }
}

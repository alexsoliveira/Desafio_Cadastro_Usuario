using FluentValidation;

namespace Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario
{
    public class UpdateUsuarioInputValidator
        : AbstractValidator<UpdateUsuarioInput>
    {
        public UpdateUsuarioInputValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

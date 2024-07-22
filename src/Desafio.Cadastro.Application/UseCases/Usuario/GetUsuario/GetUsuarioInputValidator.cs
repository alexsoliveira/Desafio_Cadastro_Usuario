using FluentValidation;

namespace Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario
{
    public class GetUsuarioInputValidator
        : AbstractValidator<GetUsuarioInput>
    {
        public GetUsuarioInputValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

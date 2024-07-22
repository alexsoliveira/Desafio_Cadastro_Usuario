using Desafio.Cadastro.Api.ApiModels.Response;
using Desafio.Cadastro.Application.UseCases.Usuario.Common;
using Desafio.Cadastro.Application.UseCases.Usuario.CreateUsuario;
using Desafio.Cadastro.Application.UseCases.Usuario.DeleteUsuario;
using Desafio.Cadastro.Application.UseCases.Usuario.GetUsuario;
using Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios;
using Desafio.Cadastro.Application.UseCases.Usuario.UpdateUsuario;
using Desafio.Cadastro.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(UsuarioModelOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreateUsuarioInput input,
            CancellationToken cancellationToken
        )
        {
            var output = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(
                nameof(Create),
                new { output.Id },
                output                
            );
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UsuarioModelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var output = await _mediator.Send(new GetUsuarioInput(id), cancellationToken);
            return Ok(output);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send(new DeleteUsuarioInput(id), cancellationToken);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UsuarioModelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUsuarioInput apiInput,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var input = new UpdateUsuarioInput(
                id,
                apiInput.Name              
            );
            var output = await _mediator.Send(input, cancellationToken);
            return Ok(output);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListUsuariosOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> List(
            CancellationToken cancellationToken,
            [FromQuery] int? page = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null
        )
        {
            var input = new ListUsuariosInput();
            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);
            return Ok(output);
        }
    }
}

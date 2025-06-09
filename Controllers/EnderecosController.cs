using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Context;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using FluentValidation;

namespace gs_sensolux.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly EnderecoUseCase _useCase;

        public EnderecosController(EnderecoUseCase enderecoUseCase)
        {
            _useCase = enderecoUseCase;
        }


        /// <summary>
        /// Lista todos os endereços.
        /// </summary>
        /// <returns>Lista de endereços.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EnderecoResponse>), 200)]
        public async Task<ActionResult<List<EnderecoResponse>>> GetEnderecos()
        {
            var result = await _useCase.ListarEnderecosAsync();
            return Ok(result);
        }

        /// <summary>
        /// Busca um endereço pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço.</param>
        /// <returns>Endereço encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnderecoResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EnderecoResponse>> GetEndereco(int id)
        {
            try
            {
                var endereco = await _useCase.BuscarPorIdAsync(id);
                return Ok(endereco);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Cria um novo endereço.
        /// </summary>
        /// <param name="request">Dados do endereço.</param>
        /// <returns>Endereço criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EnderecoResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateEnderecoRequest request)
        {
            try
            {
                var enderecoCriado = await _useCase.CriarEnderecoAsync(request);
                return CreatedAtAction(nameof(enderecoCriado.Id), new { id = enderecoCriado.Id }, enderecoCriado);
            }
            catch (ValidationException ex)
            {
                var erros = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { erros });
            }
        }

        /// <summary>
        /// Atualiza um endereço existente.
        /// </summary>
        /// <param name="id">ID do endereço a ser atualizado.</param>
        /// <param name="request">Dados atualizados.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateEnderecoRequest request)
        {
            try
            {
                await _useCase.AtualizarEnderecoAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                var erros = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { erros });
            }
        }
        /// <summary>
        /// Exclui um endereço pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser excluído.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            try
            {
                await _useCase.DeletarEnderecoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

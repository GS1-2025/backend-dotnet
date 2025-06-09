using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using FluentValidation;
using System.Linq;
using gs_sensolux.Application.DTOs.Response;

namespace gs_sensolux.API.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas aos Pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoUseCase _pedidoUseCase;

        public PedidoController(PedidoUseCase pedidoUseCase)
        {
            _pedidoUseCase = pedidoUseCase;
        }

        /// <summary>
        /// Lista todos os pedidos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var pedidos = await _pedidoUseCase.ListarTodosAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Busca um pedido pelo ID.
        /// </summary>
        [HttpGet("{id:int}", Name = "GetPedidoById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pedido = await _pedidoUseCase.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(EnderecoResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePedidoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var novoPedido = await _pedidoUseCase.CriarAsync(request);

                if (novoPedido.Id <= 0)
                    return Ok(novoPedido);

                // Agora o nome da action bate com GetByIdAsync, e rota aceita o id
                return CreatedAtRoute("GetPedidoById", new { id = novoPedido.Id }, novoPedido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                var erros = ex.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Mensagem = e.ErrorMessage
                });

                return StatusCode(500, new
                {
                    mensagem = "Erro de validação no servidor.",
                    erros
                });
            }
        }

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreatePedidoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoUseCase.AtualizarAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                var erros = ex.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Mensagem = e.ErrorMessage
                });

                return StatusCode(500, new
                {
                    mensagem = "Erro de validação no servidor.",
                    erros
                });
            }
        }

        /// <summary>
        /// Exclui um pedido pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _pedidoUseCase.ExcluirAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

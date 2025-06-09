using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using FluentValidation;

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
        /// <returns>Lista de pedidos</returns>
        /// <response code="200">Lista retornada com sucesso</response>
        [HttpGet]
        public async Task<IActionResult> ListarTodosAsync()
        {
            var pedidos = await _pedidoUseCase.ListarTodosAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Busca um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Pedido encontrado</returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="404">Pedido não encontrado</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            var pedido = await _pedidoUseCase.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="request">Dados para criação do pedido</param>
        /// <returns>Pedido criado</returns>
        /// <response code="201">Pedido criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        public async Task<IActionResult> CriarAsync([FromBody] CreatePedidoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var novoPedido = await _pedidoUseCase.CriarAsync(request);
                return CreatedAtAction(nameof(BuscarPorIdAsync), new { id = novoPedido.Id }, novoPedido);
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
                // Cria uma lista com as mensagens de erro de validação
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
        /// <param name="id">ID do pedido a ser atualizado</param>
        /// <param name="request">Dados para atualização do pedido</param>
        /// <response code="204">Pedido atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Pedido não encontrado</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarAsync(int id, [FromBody] CreatePedidoRequest request)
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
                // Cria uma lista com as mensagens de erro de validação
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
        /// <param name="id">ID do pedido a ser excluído</param>
        /// <response code="204">Pedido excluído com sucesso</response>
        /// <response code="404">Pedido não encontrado</response>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirAsync(int id)
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

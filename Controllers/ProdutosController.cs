using Microsoft.AspNetCore.Mvc;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using gs_sensolux.Application.UseCases;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using gs_sensolux.Domain.Entity;
using FluentValidation;

namespace gs_sensolux.Controllers
{
    /// <summary>
    /// Controller para gerenciar operações relacionadas a Produtos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoUseCase _produtoUseCase;

        public ProdutosController(ProdutoUseCase produtoUseCase)
        {
            _produtoUseCase = produtoUseCase;
        }

        /// <summary>
        /// Retorna a lista de todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos</returns>
        /// <response code="200">Retorna a lista de produtos</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Produto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProdutos()
        {
            try
            {
                var produtos = await _produtoUseCase.ListarTodosAsync();
                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Busca um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns>Produto encontrado</returns>
        /// <response code="200">Produto encontrado</response>
        /// <response code="404">Produto não encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Produto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                var produto = await _produtoUseCase.BuscarPorIdAsync(id);
                if (produto == null)
                    return NotFound($"Produto com id {id} não encontrado.");

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="request">Dados para criação do produto</param>
        /// <returns>Produto criado</returns>
        /// <response code="201">Produto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(Produto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CreateProdutoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var produtoCriado = await _produtoUseCase.CriarAsync(request);
                return CreatedAtAction(nameof(produtoCriado.Id), new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Extrai as mensagens de erro do FluentValidation e retorna no corpo da resposta
                var erros = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { erros });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

            /// <summary>
            /// Atualiza um produto existente.
            /// </summary>
            /// <param name="id">ID do produto a ser atualizado</param>
            /// <param name="request">Dados para atualização do produto</param>
            /// <response code="204">Produto atualizado com sucesso</response>
            /// <response code="400">Dados inválidos</response>
            /// <response code="404">Produto não encontrado</response>
            /// <response code="500">Erro interno no servidor</response>
            [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateProdutoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _produtoUseCase.AtualizarAsync(id, request);
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
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido</param>
        /// <response code="204">Produto removido com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoUseCase.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using gs_sensolux.Application.UseCases;

namespace gs_sensolux.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensoresController : ControllerBase
    {
        private readonly SensorUseCase _sensorUseCase;

        public SensoresController(SensorUseCase sensorUseCase)
        {
            _sensorUseCase = sensorUseCase;
        }

        /// <summary>
        /// Lista todos os sensores cadastrados.
        /// </summary>
        /// <returns>Lista de sensores.</returns>
        /// <response code="200">Retorna a lista de sensores.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<SensorResponse>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSensores()
        {
            try
            {
                var sensores = await _sensorUseCase.ListarSensoresAsync();
                return Ok(sensores);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Busca um sensor pelo seu ID.
        /// </summary>
        /// <param name="id">ID do sensor.</param>
        /// <returns>Detalhes do sensor.</returns>
        /// <response code="200">Retorna o sensor encontrado.</response>
        /// <response code="404">Sensor não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SensorResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSensor(int id)
        {
            try
            {
                var sensor = await _sensorUseCase.BuscarPorIdAsync(id);
                return Ok(sensor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Sensor com id {id} não encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Cria um novo sensor.
        /// </summary>
        /// <param name="request">Dados para criação do sensor.</param>
        /// <returns>O sensor criado.</returns>
        /// <response code="201">Sensor criado com sucesso.</response>
        /// <response code="400">Dados inválidos na requisição.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(SensorResponse), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostSensor([FromBody] CreateSensorRequest request)
        {
            try
            {
                var sensorCriado = await _sensorUseCase.CriarSensorAsync(request);
                return CreatedAtAction(nameof(GetSensor), new { id = sensorCriado.Id }, sensorCriado);
            }
            catch (ValidationException vex)
            {
                var erros = vex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = erros });
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException vex)
            {
                return BadRequest(new { Errors = new[] { vex.Message } });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Atualiza um sensor existente.
        /// </summary>
        /// <param name="id">ID do sensor a ser atualizado.</param>
        /// <param name="request">Dados para atualização do sensor.</param>
        /// <response code="204">Sensor atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos na requisição.</response>
        /// <response code="404">Sensor não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutSensor(int id, [FromBody] CreateSensorRequest request)
        {
            try
            {
                await _sensorUseCase.AtualizarSensorAsync(id, request);
                return NoContent();
            }
            catch (ValidationException vex)
            {
                var erros = vex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = erros });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Sensor com id {id} não encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Remove um sensor pelo seu ID.
        /// </summary>
        /// <param name="id">ID do sensor a ser removido.</param>
        /// <response code="204">Sensor removido com sucesso.</response>
        /// <response code="404">Sensor não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            try
            {
                await _sensorUseCase.DeletarSensorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Sensor com id {id} não encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}

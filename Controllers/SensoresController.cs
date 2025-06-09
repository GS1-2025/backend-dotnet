    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using FluentValidation.Results;
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
                catch (Exception)
                {
                    return StatusCode(500, "Erro interno no servidor.");
                }
            }

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

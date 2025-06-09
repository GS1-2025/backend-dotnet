using FluentValidation;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SensorUseCase
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateSensorRequest> _validator;

    public SensorUseCase(AppDbContext context, IValidator<CreateSensorRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<SensorResponse> CriarSensorAsync(CreateSensorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sensor = new Sensor(request.Tipo, request.Modelo, request.Descricao);

        // Define status conforme o que vier na requisição (padrão: Ativo)
        if (!string.IsNullOrEmpty(request.Status))
        {
            var statusLower = request.Status.ToLowerInvariant();
            if (statusLower == "inativo")
                sensor.Desativar();
            else
                sensor.Ativar();
        }
        else
        {
            sensor.Ativar(); // status padrão
        }

        _context.Sensores.Add(sensor);
        await _context.SaveChangesAsync();

        return MapToResponse(sensor);
    }

    public async Task<List<SensorResponse>> ListarSensoresAsync()
    {
        var sensores = await _context.Sensores.ToListAsync();
        return sensores.Select(MapToResponse).ToList();
    }

    public async Task<SensorResponse> BuscarPorIdAsync(int id)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null) throw new KeyNotFoundException("Sensor não encontrado");
        return MapToResponse(sensor);
    }

    public async Task AtualizarSensorAsync(int id, CreateSensorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null) throw new KeyNotFoundException("Sensor não encontrado");

        sensor.SetTipo(request.Tipo);
        sensor.SetModelo(request.Modelo);
        sensor.SetDescricao(request.Descricao);

        if (!string.IsNullOrEmpty(request.Status))
        {
            var statusLower = request.Status.ToLowerInvariant();
            if (statusLower == "ativo")
                sensor.Ativar();
            else if (statusLower == "inativo")
                sensor.Desativar();
        }

        _context.Entry(sensor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarSensorAsync(int id)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null) throw new KeyNotFoundException("Sensor não encontrado");

        _context.Sensores.Remove(sensor);
        await _context.SaveChangesAsync();
    }

    private SensorResponse MapToResponse(Sensor sensor)
    {
        return new SensorResponse
        {
            Id = sensor.Id,
            ProdutoId = sensor.ProdutoId,
            Tipo = sensor.Tipo,
            Modelo = sensor.Modelo,
            Descricao = sensor.Descricao,
            Status = sensor.Status
        };
    }
}

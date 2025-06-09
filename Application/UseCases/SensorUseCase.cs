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
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var produtoExiste = await _context.Produtos
         .Where(p => p.Id == request.ProdutoId)
         .Select(p => 1)
         .FirstOrDefaultAsync() == 1;

        if (!produtoExiste)
            throw new KeyNotFoundException($"Produto com ID {request.ProdutoId} não encontrado.");

        var sensor = new Sensor(request.Tipo, request.Modelo, request.Descricao);
        SetProdutoId(sensor, request.ProdutoId);

        switch (request.Status?.ToLowerInvariant())
        {
            case "inativo":
                sensor.Desativar();
                break;
            case "ativo":
            case null:
            case "":
                sensor.Ativar();
                break;
            default:
                throw new ArgumentException($"Status '{request.Status}' inválido. Use 'ativo' ou 'inativo'.");
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

        // Validar se Produto existe
        var produtoExiste = await _context.Produtos
         .Where(p => p.Id == request.ProdutoId)
         .Select(p => 1)
         .FirstOrDefaultAsync() == 1;

        if (!produtoExiste)
            throw new KeyNotFoundException($"Produto com ID {request.ProdutoId} não encontrado.");

        sensor.SetTipo(request.Tipo);
        sensor.SetModelo(request.Modelo);
        sensor.SetDescricao(request.Descricao);

        // Atualizar ProdutoId
        SetProdutoId(sensor, request.ProdutoId);

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

    private void SetProdutoId(Sensor sensor, int produtoId)
    {
        var prop = typeof(Sensor).GetProperty("ProdutoId", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(sensor, produtoId);
        }
        else
        {
            throw new System.Exception("Não foi possível setar ProdutoId no Sensor.");
        }
    }
}

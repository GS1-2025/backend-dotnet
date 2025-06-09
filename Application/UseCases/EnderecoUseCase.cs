using FluentValidation;
using FluentValidation.Results;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class EnderecoUseCase
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateEnderecoRequest> _validator;

    public EnderecoUseCase(AppDbContext context, IValidator<CreateEnderecoRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<EnderecoResponse> CriarEnderecoAsync(CreateEnderecoRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var endereco = new Endereco(
            request.Cep,
            request.Estado,
            request.Cidade,
            request.Bairro,
            request.Rua
        );

        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();

        return MapToResponse(endereco);
    }

    public async Task<List<EnderecoResponse>> ListarEnderecosAsync()
    {
        var enderecos = await _context.Enderecos.ToListAsync();
        return enderecos.Select(MapToResponse).ToList();
    }

    public async Task<EnderecoResponse> BuscarPorIdAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco == null) throw new KeyNotFoundException("Endereço não encontrado");
        return MapToResponse(endereco);
    }

    public async Task AtualizarEnderecoAsync(int id, CreateEnderecoRequest request)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco == null) throw new KeyNotFoundException("Endereço não encontrado");

        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        endereco.SetCep(request.Cep);
        endereco.SetEstado(request.Estado);
        endereco.SetCidade(request.Cidade);
        endereco.SetBairro(request.Bairro);
        endereco.SetRua(request.Rua);

        _context.Entry(endereco).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarEnderecoAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco == null) throw new KeyNotFoundException("Endereço não encontrado");

        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();
    }

    private EnderecoResponse MapToResponse(Endereco endereco)
    {
        return new EnderecoResponse
        {
            Id = endereco.Id,
            Cep = endereco.Cep,
            Estado = endereco.Estado,
            Cidade = endereco.Cidade,
            Bairro = endereco.Bairro,
            Rua = endereco.Rua
        };
    }
}

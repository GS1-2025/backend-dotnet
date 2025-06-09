using FluentValidation;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace gs_sensolux.Application.UseCases
{
    public class ProdutoUseCase
    {
        private readonly AppDbContext _context;

        private readonly IValidator<CreateProdutoRequest> _validator;

        public ProdutoUseCase(AppDbContext context, IValidator<CreateProdutoRequest> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> BuscarPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> CriarAsync(CreateProdutoRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                var produto = new Produto(request.Nome, request.Descricao, request.PrecoUnitario);

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no ProdutoUseCase.CriarAsync: {ex.Message}");
                throw;
            }
        }


        public async Task AtualizarAsync(int id, CreateProdutoRequest request)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            produto.GetType().GetProperty("Nome")?.SetValue(produto, request.Nome);
            produto.GetType().GetProperty("Descricao")?.SetValue(produto, request.Descricao);
            produto.GetType().GetProperty("PrecoUnitario")?.SetValue(produto, request.PrecoUnitario);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}

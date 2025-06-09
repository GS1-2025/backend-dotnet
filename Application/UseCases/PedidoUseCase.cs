using FluentValidation;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.DTOs.Response;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace gs_sensolux.Application.UseCases
{
    public class PedidoUseCase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreatePedidoRequest> _validator;

        public PedidoUseCase(AppDbContext context, IValidator<CreatePedidoRequest> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<List<PedidoResponse>> ListarTodosAsync()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produtos)
                .ToListAsync();

            return pedidos.Select(pedido =>
            {
                ItensPedidoResponse? itensResponse = null;

                if (pedido.Itens != null)
                {
                    itensResponse = new ItensPedidoResponse
                    {
                        Id = pedido.Itens.Id,
                        PedidoId = pedido.Id,
                        UsuarioId = pedido.Itens.UsuarioId,
                        Quantidade = pedido.Itens.Quantidade,
                        ProdutoIds = pedido.Itens.Produtos?.Select(prod => prod.Id).ToList() ?? new List<int>()
                    };
                }

                return new PedidoResponse
                {
                    Id = pedido.Id,
                    DataPedido = pedido.DataPedido,
                    Status = pedido.Status,
                    Preco = pedido.Preco,
                    Itens = itensResponse
                };
            }).ToList();
        }

        public async Task<PedidoResponse?> BuscarPorIdAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return null;

            if (pedido.Itens == null)
            {
                return new PedidoResponse
                {
                    Id = pedido.Id,
                    DataPedido = pedido.DataPedido,
                    Status = pedido.Status,
                    Preco = pedido.Preco,
                    Itens = null
                };
            }

            var itens = pedido.Itens;

            return new PedidoResponse
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                Status = pedido.Status,
                Preco = pedido.Preco,
                Itens = new ItensPedidoResponse
                {
                    Id = itens.Id,
                    PedidoId = pedido.Id,
                    UsuarioId = itens.UsuarioId,
                    Quantidade = itens.Quantidade,
                    ProdutoIds = itens.Produtos?.Select(prod => prod.Id).ToList() ?? new List<int>()
                }
            };
        }

        public async Task<PedidoResponse> CriarAsync(CreatePedidoRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (request == null)
                throw new ArgumentException("Requisição inválida.");

            var novoPedido = new Pedido(request.DataPedido, "Em Aberto");

            var itensReq = request.Itens;
            if (itensReq != null)
            {
                var novoItem = new ItensPedido(itensReq.UsuarioId, itensReq.Quantidade);

                foreach (var produtoId in itensReq.ProdutoIds)
                {
                    var produto = await _context.Produtos.FindAsync(produtoId);
                    if (produto == null)
                        throw new KeyNotFoundException($"Produto ID {produtoId} não encontrado.");

                    novoItem.AdicionarProduto(produto);
                }

                novoPedido.AdicionarItem(novoItem);
            }

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();

            var itens = novoPedido.Itens;

            ItensPedidoResponse? itensResponse = null;

            if (itens != null)
            {
                itensResponse = new ItensPedidoResponse
                {
                    Id = itens.Id,
                    PedidoId = novoPedido.Id,
                    UsuarioId = itens.UsuarioId,
                    Quantidade = itens.Quantidade,
                    ProdutoIds = itens.Produtos?.Select(prod => prod.Id).ToList() ?? new List<int>()
                };
            }

            return new PedidoResponse
            {
                Id = novoPedido.Id,
                DataPedido = novoPedido.DataPedido,
                Status = novoPedido.Status,
                Preco = novoPedido.Preco,
                Itens = itensResponse
            };
        }

        public async Task AtualizarAsync(int id, CreatePedidoRequest request)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            pedido.Atualizar(request.DataPedido, pedido.Status);

            await _context.SaveChangesAsync();
        }



    

public async Task ExcluirAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.Itens != null)
            {
                pedido.Itens.Produtos?.Clear(); 
                _context.ItensPedido.Remove(pedido.Itens);
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }


    }
}
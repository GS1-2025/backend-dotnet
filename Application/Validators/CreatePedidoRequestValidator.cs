using FluentValidation;
using gs_sensolux.Application.DTOs.Request;

namespace gs_sensolux.Application.Validators
{
    public class CreatePedidoRequestValidator : AbstractValidator<CreatePedidoRequest>
    {
        public CreatePedidoRequestValidator()
        {
            RuleFor(x => x.DataPedido)
                .NotEmpty().WithMessage("A data do pedido é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data do pedido não pode ser no futuro.");

            RuleFor(x => x.Itens)
                .NotNull().WithMessage("É necessário incluir os dados dos itens do pedido.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Itens.UsuarioId)
                        .GreaterThan(0).WithMessage("O ID do usuário deve ser válido.");

                    RuleFor(x => x.Itens.Quantidade)
                        .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

                    RuleFor(x => x.Itens.ProdutoIds)
                        .NotNull().WithMessage("É necessário fornecer os produtos do pedido.")
                        .Must(produtos => produtos.Count > 0).WithMessage("Adicione pelo menos um produto ao pedido.");
                });
        }
    }

    }

using FluentValidation;
using gs_sensolux.Application.DTOs.Request;

namespace gs_sensolux.Application.Validators
{
    public class CreateProdutoRequestValidator : AbstractValidator<CreateProdutoRequest>
    {
        public CreateProdutoRequestValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(20).WithMessage("O nome deve ter no máximo 20 caracteres.");

            RuleFor(p => p.Descricao)
                .MaximumLength(50).WithMessage("A descrição deve ter no máximo 50 caracteres.");

            RuleFor(p => p.PrecoUnitario)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
        }
    }
}

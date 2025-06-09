using FluentValidation;
using gs_sensolux.Application.DTOs.Request;

namespace gs_sensolux.Application.Validators
{
    public class CreateSensorRequestValidator : AbstractValidator<CreateSensorRequest>
    {
        public CreateSensorRequestValidator()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("Tipo do sensor é obrigatório.");

            RuleFor(x => x.Modelo)
                .MaximumLength(100).WithMessage("Modelo deve ter no máximo 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Modelo));

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Descricao));

            RuleFor(x => x.Status)
                .Must(s => s == null ||
                           s.Equals("Ativo", StringComparison.OrdinalIgnoreCase) ||
                           s.Equals("Inativo", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Status deve ser 'Ativo' ou 'Inativo', se informado.");
        }
    }
}


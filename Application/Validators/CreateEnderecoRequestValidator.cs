using FluentValidation;
using gs_sensolux.Application.DTOs.Request;

namespace gs_sensolux.Application.Validators
{
    public class CreateEnderecoRequestValidator : AbstractValidator<CreateEnderecoRequest>
    {
        public CreateEnderecoRequestValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Length(8).WithMessage("O CEP deve conter exatamente 8 caracteres.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .MaximumLength(2).WithMessage("O estado deve conter no máximo 2 caracteres.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(50).WithMessage("A cidade deve conter no máximo 50 caracteres.");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.")
                .MaximumLength(50).WithMessage("O bairro deve conter no máximo 50 caracteres.");

            RuleFor(x => x.Rua)
                .NotEmpty().WithMessage("A rua é obrigatória.")
                .MaximumLength(100).WithMessage("A rua deve conter no máximo 100 caracteres.");

            RuleFor(x => x.UsuarioId)
                .GreaterThan(0).WithMessage("O ID do usuário deve ser válido.");
        }
    }

}

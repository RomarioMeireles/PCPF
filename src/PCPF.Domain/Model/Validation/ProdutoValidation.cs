using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(a => a.Descricao)
   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
   .Length(1, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.CodigoBarras)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(6, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.QuantidadeMinima)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.Valor)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
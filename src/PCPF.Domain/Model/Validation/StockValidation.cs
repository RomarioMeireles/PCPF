using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
   public class StockValidation:AbstractValidator<Stock>
    {
        public StockValidation()
        {
            //RuleFor(a => a.ProdutoId)
            //   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.NumeroLote)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(3, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}

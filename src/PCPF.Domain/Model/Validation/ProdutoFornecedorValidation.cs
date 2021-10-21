

using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
   public class ProdutoFornecedorValidation : AbstractValidator<ProdutoFornecedor>
    {
        public ProdutoFornecedorValidation()
        {
            RuleFor(a => a.ProdutoId)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.FornecedorId)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
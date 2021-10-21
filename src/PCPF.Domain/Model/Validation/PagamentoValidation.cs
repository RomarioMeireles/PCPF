using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
    public class PagamentoValidation : AbstractValidator<Pagamento>
    {
        public PagamentoValidation()
        {
            RuleFor(a => a.ValotTotal)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}


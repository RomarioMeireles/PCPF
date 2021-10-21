using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
   public class PedidoItemValidation : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation()
        {
            RuleFor(a => a.Quantidade)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.Valor)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}

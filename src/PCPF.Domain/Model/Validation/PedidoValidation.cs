using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
  public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(a => a.ClienteId)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
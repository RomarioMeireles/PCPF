using FluentValidation;


namespace PCPF.Domain.Model.Validation
{
   public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(a => a.DenominacaoFiscal)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(5, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.Telefone)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(6, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.Email)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.Endereco)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}

using FluentValidation;


namespace PCPF.Domain.Model.Validation
{
   public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(a => a.Nome)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(5, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.Telefone)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(6, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(a => a.Email)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(a => a.DocumentoIdentificacao)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
         }
    }
}

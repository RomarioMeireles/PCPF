using FluentValidation;

namespace PCPF.Domain.Model.Validation
{
    public class UserValidation : AbstractValidator<Utilizador>
    {
        public UserValidation()
        {
            RuleFor(a => a.UserName)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(6, 30).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            //RuleFor(a => a.Password)
            //   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            //   .Length(8, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}

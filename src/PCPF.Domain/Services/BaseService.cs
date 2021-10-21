using FluentValidation;
using FluentValidation.Results;
using PCPF.Domain.Model;
using PCPF.Domain.Notificacoes;

namespace PCPF.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _INotificador;

        protected BaseService(INotificador iNotificador)
        {
            _INotificador = iNotificador;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _INotificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        protected bool ExecutarValidacaoSimples<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}

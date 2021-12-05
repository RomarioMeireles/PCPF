using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Model;
using PCPF.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCPF.Web.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        protected Guid UtilizadorId = Guid.Parse("D47EAAF3-1713-4A0B-8EF1-2C3995B90A7F");
        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            if (!_notificador.TemNotificacao()) return true;

            var notificacoes = _notificador.ObterNotificacoes();
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));
            return false;
        }
        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList();
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
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}

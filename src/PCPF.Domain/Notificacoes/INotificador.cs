using System.Collections.Generic;

namespace PCPF.Domain.Notificacoes
{
   public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}

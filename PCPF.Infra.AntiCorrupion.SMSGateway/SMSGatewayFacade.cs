using PCPF.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace PCPF.Infra.AntiCorrupion.SMSGateway
{
    public class SMSGatewayFacade : ISMSGatewayFacade
    {
        public async Task<bool> Enviar(string destino, string mensagem)
        {
            throw new NotImplementedException();
        }
    }
}

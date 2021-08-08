using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
    public interface ISMSGatewayFacade
    {
        Task<bool> Enviar(string destino, string mensagem);
    }
}

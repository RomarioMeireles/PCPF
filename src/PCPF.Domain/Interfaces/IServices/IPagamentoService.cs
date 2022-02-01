using PCPF.Domain.Model;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
    public interface IPagamentoService
    {
        Task Adicionar(Pagamento entity, int idPedido);
        Task Atualizar(Pagamento entity);
        Task Remover(int id);
    }
}

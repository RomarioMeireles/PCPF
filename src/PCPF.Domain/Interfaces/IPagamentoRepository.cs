using PCPF.Domain.Model;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
   public interface IPagamentoRepository:IRepository<Pagamento>
    {
        Task Adicionar(Pagamento pagamento, Pedido pedido);
    }
}

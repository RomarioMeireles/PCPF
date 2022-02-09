using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<IEnumerable<Stock>> ObterStockBaixo();
        Task DebitarStock(Pedido pedido);
    }
}

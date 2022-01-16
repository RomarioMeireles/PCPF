
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho);
        Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorSessaoId(string sessaoId);
        Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorUserName(string userName);
    }
}

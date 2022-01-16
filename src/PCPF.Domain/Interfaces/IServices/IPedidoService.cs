using PCPF.Domain.Model;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
    public interface IPedidoService
    {
        Task Adicionar(Pedido entity);
        Task Atualizar(Pedido entity);
        Task Remover(int id);
        Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho);
    }
}

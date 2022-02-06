using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
    public interface IPedidoService
    {
        Task Adicionar(Pedido entity);
        Task Atualizar(Pedido entity);
        Task Remover(int id);
        Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho);
        Task ActualizarPedidoRascunho(IEnumerable<PedidoRascunho> pedido);
        void CriarPedido(IEnumerable<PedidoRascunho> pedidoRascunhos);
        Task RemoverItemRascunho(int id);
        Task Cancelar(int id, string observacao, bool sms);
    }
}

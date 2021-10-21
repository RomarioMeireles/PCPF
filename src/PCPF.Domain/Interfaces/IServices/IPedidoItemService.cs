using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IPedidoItemService
    {
        Task Adicionar(PedidoItem entity);
        Task Atualizar(PedidoItem entity);
        Task Remover(int id);
    }
}

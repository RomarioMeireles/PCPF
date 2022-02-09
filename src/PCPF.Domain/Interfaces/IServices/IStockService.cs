using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IStockService
    {
        Task Adicionar(Stock entity);
        Task Atualizar(Stock entity);
        Task Remover(int id);
        Task CreditarStock(int stockId, int quantidade);
    }
}

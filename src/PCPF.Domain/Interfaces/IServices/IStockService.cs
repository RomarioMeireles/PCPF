using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
    interface IStockService
    {
        Task Adicionar(Stock entity);
        Task Atualizar(Stock entity);
        Task Remover(int id);
    }
}

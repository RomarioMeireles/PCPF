using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IFornecedorService
    {
        Task Adicionar(Fornecedor entity);
        Task Atualizar(Fornecedor entity);
        Task Remover(int id);
    }
}

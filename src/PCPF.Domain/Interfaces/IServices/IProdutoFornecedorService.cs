using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IProdutoFornecedorService
    {
        Task Adicionar(ProdutoFornecedor entity);
        Task Atualizar(ProdutoFornecedor entity);
        Task Remover(int id);
    }
}

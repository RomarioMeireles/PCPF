using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IProdutoService
    {
        Task Adicionar(Produto entity);
        Task Atualizar(Produto entity);
        Task Remover(int id);
    }
}

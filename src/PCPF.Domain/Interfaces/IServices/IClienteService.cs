using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IClienteService
    {
        Task Adicionar(Cliente entity);
        Task Atualizar(Cliente entity);
        Task Remover(int id);
    }
}

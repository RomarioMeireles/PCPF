using PCPF.Domain.Model;
using System;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
   public interface IUtilizadorService
    {
        Task Adicionar(Utilizador entity);
        Task Atualizar(Utilizador entity);
        Task Remover(int id);
    }
}

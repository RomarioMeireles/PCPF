using PCPF.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces.IServices
{
    public interface IPagamentoService
    {
        Task Adicionar(Pagamento entity);
        Task Atualizar(Pagamento entity);
        Task Remover(int id);
    }
}

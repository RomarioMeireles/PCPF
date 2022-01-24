using PCPF.Domain.Model;

namespace PCPF.Domain.Interfaces
{
    public interface IClienteRepository:IRepository<Cliente>
    {
        void AdicionarCliente(Cliente cliente, Utilizador utilizador);
    }
}

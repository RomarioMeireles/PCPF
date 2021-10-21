using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
    public class ClienteRepository:Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(PCPFContext db) : base(db)
        {
        }
    }
}

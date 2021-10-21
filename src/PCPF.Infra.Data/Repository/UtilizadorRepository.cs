using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
   public class UtilizadorRepository:Repository<Utilizador>, IUtilizadorRepository
    {
        public UtilizadorRepository(PCPFContext db) : base(db)
        {
        }
    }
}

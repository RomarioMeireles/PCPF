using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
   public class PagamentoRepository:Repository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(PCPFContext db) : base(db)
        {

        }
    }
}

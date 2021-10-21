
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
   public class PedidoRepository: Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PCPFContext db) : base(db)
        {

        }
    }
}

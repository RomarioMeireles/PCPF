using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
    public class PedidoItemRepository:Repository<PedidoItem>,IPedidoItemRepository
    {
        public PedidoItemRepository(PCPFContext db) : base(db)
        {

        }
    }
}

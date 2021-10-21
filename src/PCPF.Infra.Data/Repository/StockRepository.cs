using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
    public class StockRepository:Repository<Stock>, IStockRepository
    {
        public StockRepository(PCPFContext db) : base(db)
        {

        }
    }
}

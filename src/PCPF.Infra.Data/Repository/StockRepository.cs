using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Infra.Data.Repository
{
    public class StockRepository:Repository<Stock>, IStockRepository
    {
        public StockRepository(PCPFContext db) : base(db)
        {

        }
        public override async Task<IEnumerable<Stock>> ObterTodos()
        {
            return await DbSet.Include(a=>a.Produto).Where(a=>a.Status==true).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PCPF.Infra.Data.Repository
{
   public class PagamentoRepository:Repository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(PCPFContext db) : base(db)
        {

        }

        public Task Adicionar(Pagamento pagamento, Pedido pedido)
        {
            using(var transaction = new TransactionScope())
            {
                DbSet.Add(pagamento);
                Db.SaveChanges();
                Db.Pedido.Update(pedido);
                Db.SaveChanges();
                transaction.Complete();
            }
            return Task.CompletedTask;
        }
        public override async Task<IEnumerable<Pagamento>> ObterTodos()
        {
            return await DbSet.Include(a => a.Pedido).Include(a => a.Pedido.Cliente).Where(a=>a.Status==true).AsNoTrackingWithIdentityResolution().ToListAsync();
        }
    }
}

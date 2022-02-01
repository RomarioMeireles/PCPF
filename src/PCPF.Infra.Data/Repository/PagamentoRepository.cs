using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Threading.Tasks;
using System.Transactions;

namespace PCPF.Infra.Data.Repository
{
   public class PagamentoRepository:Repository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(PCPFContext db) : base(db)
        {

        }

        public async Task Adicionar(Pagamento pagamento, Pedido pedido)
        {
            using(var transaction = new TransactionScope())
            {
                DbSet.Add(pagamento);
                await SaveChanges();
                Db.Pedido.Update(pedido);
                await SaveChanges();
            }
        }
    }
}

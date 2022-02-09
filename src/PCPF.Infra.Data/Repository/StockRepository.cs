using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

        public async Task<IEnumerable<Stock>> ObterStockBaixo()
        {
            return await Db.Stock.FromSqlInterpolated($@"
                SELECT 
	                  t1.[Id]
		                ,[Quantidade]
		                ,[ProdutoId]
		                ,[NumeroLote]
		                ,[DataValidade]
		                ,t1.[UtilizadorId]
                      ,t1.[Status]
                      ,t1.[DataRegisto]
                FROM 
	                [PCPF].[dbo].[Stock] as t1
                inner join
	                [PCPF].[dbo].Produto as t2
	                on t1.ProdutoId=t2.Id
                where t2.QuantidadeMinima > t1.Quantidade and t1.Status=1
            ").ToListAsync();
        }

        public Task DebitarStock(Pedido pedido)
        {
            using(var transaction = new TransactionScope())
            {
                var stockList = new List<Stock>();
                foreach(var item in pedido.ItensPedido)
                {
                    var stock = Db.Stock.FirstOrDefault(a => a.ProdutoId == item.ProdutoId);
                    stock.Quantidade = stock.Quantidade - item.Quantidade;
                    stockList.Add(stock);
                }

                foreach(var item in stockList)
                {
                    Db.Database.ExecuteSqlInterpolated($"update Stock set Quantidade={item.Quantidade} where ProdutoId={item.ProdutoId}");
                }

                Db.Pedido.Update(pedido);
                Db.SaveChanges();

                transaction.Complete();

                return Task.CompletedTask;
            }
        }
    }
}

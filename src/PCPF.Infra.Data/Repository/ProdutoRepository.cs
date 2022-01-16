using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PCPF.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(PCPFContext db) : base(db)
        {

        }

        public void Adicionar(Produto produto, Stock stock)
        {
            using (var scope = new TransactionScope())
            {
                Db.Produto.Add(produto);
                Db.SaveChanges();

                stock.SetProdutoId(produto.Id);

                Db.Stock.AddRange(stock);
                Db.SaveChanges();

                scope.Complete();
            }
        }

        public async Task<string> ObterImagem(int id)
        {
            return await DbSet.Where(c=>c.Id==id).Select(b=>b.Imagem).FirstOrDefaultAsync();
        }
    }
}

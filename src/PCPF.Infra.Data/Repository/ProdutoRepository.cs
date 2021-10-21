using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
   public class ProdutoRepository:Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(PCPFContext db) : base(db)
        {

        }
    }
}

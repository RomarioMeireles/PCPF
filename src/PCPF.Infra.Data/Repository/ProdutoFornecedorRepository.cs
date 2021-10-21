using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
    public class ProdutoFornecedorRepository:Repository<ProdutoFornecedor>, IProdutoFornecedorRepository
    {
        public ProdutoFornecedorRepository(PCPFContext db) : base(db)
        {

        }
    }
}

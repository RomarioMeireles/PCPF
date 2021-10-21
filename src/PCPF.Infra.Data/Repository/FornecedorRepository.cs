using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
   public class FornecedorRepository:Repository<Fornecedor>,IFornecedorRepository
    {
        public FornecedorRepository(PCPFContext db) : base(db)
        {
        }
    }
}

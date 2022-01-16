
using PCPF.Domain.Model;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void Adicionar(Produto produto, Stock stock);
        Task<string> ObterImagem(int id);
    }
}

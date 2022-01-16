
using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Infra.Data.Repository
{
   public class PedidoRepository: Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PCPFContext db) : base(db)
        {

        }

        public async Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho)
        {
            Db.PedidoRascunho.Add(pedidoRascunho);
            await SaveChanges();
        }

        public async Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorSessaoId(string sessaoId)
        {
            return await Db.PedidoRascunho.Where(a => a.SessionId == sessaoId).ToListAsync();

        }

        public async Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorUserName(string userName)
        {
            return await Db.PedidoRascunho.Where(a => a.UserName == userName).ToListAsync();
        }
    }
}

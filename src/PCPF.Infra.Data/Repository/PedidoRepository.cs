using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PCPF.Infra.Data.Repository
{
   public class PedidoRepository: Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PCPFContext db) : base(db)
        {

        }

        public async Task ActualizarPedidoRascunho(IEnumerable<PedidoRascunho> pedido)
        {
            Db.PedidoRascunho.UpdateRange(pedido);
            await SaveChanges();
        }

        public async Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho)
        {
            Db.PedidoRascunho.Add(pedidoRascunho);
            await SaveChanges();
        }

        public void CriarPedido(IEnumerable<PedidoRascunho> pedidoRascunho, Pedido pedido)
        {
            using(var transaction = new TransactionScope())
            {
                var clienteId = Db.Cliente.FirstOrDefault(a=>a.UserName==pedidoRascunho.FirstOrDefault().UserName).Id;
                pedido.ClienteId=clienteId;
                DbSet.Add(pedido);
                Db.SaveChanges();
                Db.PedidoRascunho.RemoveRange(pedidoRascunho);
                Db.SaveChanges();

                transaction.Complete();
            }
        }

        public async Task<IEnumerable<Pedido>> ObterPedidoPorUserName(string userName)
        {
            var clienteId = await Db.Cliente.Where(a => a.UserName == userName).Select(b => b.Id).FirstOrDefaultAsync();
            return await DbSet.Include(b=>b.ItensPedido).Where(a => a.ClienteId == clienteId).ToListAsync();
        }

        public async Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorSessaoId(string sessaoId)
        {
            return await Db.PedidoRascunho.Where(a => a.SessionId == sessaoId).ToListAsync();
        }

        public async Task<IEnumerable<PedidoRascunho>> ObterPedidoRascunhoPorUserName(string userName)
        {
            return await Db.PedidoRascunho.Where(a => a.UserName == userName).ToListAsync();
        }

        public async Task RemoverItemRascunho(int id)
        {
            Db.PedidoRascunho.Remove(await Db.PedidoRascunho.FirstOrDefaultAsync(a => a.Id == id));
            await SaveChanges();
        }
        public async Task<IEnumerable<PedidoItem>> ObterPedidoItemPorIdPedido(int idPedido)
        {
            return await Db.PedidoItem.Include(a => a.Pedido).Include(b => b.Produto).Include(a=>a.Pedido.Cliente).AsNoTrackingWithIdentityResolution().Where(a=>a.PedidoId== idPedido).ToListAsync();
        }
        public override async Task<IEnumerable<Pedido>> ObterTodos()
        {
            return await DbSet.Include(a => a.ItensPedido).Include(a => a.Cliente).AsNoTrackingWithIdentityResolution().Where(a => a.Status == true && a.Cliente.Status == true).ToListAsync();
        }
        //public override async Task<Pedido> ObterPorId(int id)
        //{
        //    return await DbSet.Include(a => a.ItensPedido).Where(a => a.Status == true).FirstOrDefaultAsync();
        //}
    }
}

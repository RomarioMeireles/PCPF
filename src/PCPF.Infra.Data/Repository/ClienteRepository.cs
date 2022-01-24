using PCPF.Domain.Interfaces;
using PCPF.Domain.Model;
using System.Transactions;

namespace PCPF.Infra.Data.Repository
{
    public class ClienteRepository:Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(PCPFContext db) : base(db)
        {
        }

        public void AdicionarCliente(Cliente cliente, Utilizador utilizador)
        {
            using(var transaction = new TransactionScope())
            {
                Db.Cliente.Add(cliente);
                Db.SaveChanges();
                Db.Utilizador.Add(utilizador);
                Db.SaveChanges();

                transaction.Complete();
            }
        }
    }
}

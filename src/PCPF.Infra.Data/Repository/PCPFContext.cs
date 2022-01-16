using Microsoft.EntityFrameworkCore;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Repository
{
    public class PCPFContext:DbContext
    {
        public PCPFContext(DbContextOptions<PCPFContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }
        public DbSet<ProdutoFornecedor> ProdutoFornecedor { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<PedidoRascunho> PedidoRascunho { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PCPFContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

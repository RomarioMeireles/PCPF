using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class PagamentoMap : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataRegisto).HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.Observacao).HasColumnType("varchar(200)");
            builder.Property(a => a.Status).HasColumnName("bit");
            builder.Property(a => a.ValotTotal).HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Pedido)
                .WithMany(b => b.Pagamentos)
                .HasForeignKey(a=>a.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class PedidoItemMap : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItem");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataRegisto).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.Desconto).HasColumnType("decimal(18,2)");
            builder.Property(a => a.PedidoId).HasColumnType("int");
            builder.Property(a => a.ProdutoId).HasColumnType("int");
            builder.Property(a => a.Quantidade).HasColumnType("int").IsRequired();
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.Valor).HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Pedido)
                .WithMany(b => b.ItensPedido)
                .HasForeignKey(a => a.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Produto)
                .WithMany(b => b.ItensPedido)
                .HasForeignKey(a => a.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ClienteId).HasColumnType("int");
            builder.Property(a => a.DataRegisto).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.FinalizadoEm).HasColumnType("datetime").IsRequired(false);
            builder.Property(a => a.IniciadoEm).HasColumnType("datetime").IsRequired();
            builder.Property(a => a.Observacao).HasColumnType("varchar(200)");
            builder.Property(a => a.Referencia).HasColumnType("bigint");
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.StatusPedido).HasConversion<string>().HasMaxLength(15).IsRequired();

            builder.HasOne(a => a.Cliente)
                .WithMany(b => b.ItensPedido)
                .HasForeignKey(a => a.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

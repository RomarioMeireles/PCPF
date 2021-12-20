using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.NumeroLote).HasColumnType("varchar(20)");
            builder.Property(a => a.ProdutoId).HasColumnType("int");
            builder.Property(a => a.Quantidade).HasColumnType("int").IsRequired();
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.UtilizadorId).HasColumnType("int").IsRequired();
            builder.Property(a => a.DataRegisto).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.DataValidade).HasColumnType("date");

            builder.HasOne(a => a.Utilizador)
                .WithMany(b => b.Stocks)
                .HasForeignKey(a => a.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

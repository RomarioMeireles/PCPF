using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.CodigoBarras).HasColumnType("varchar(128)");
            builder.Property(a => a.DataRegisto).HasColumnType("datetime");
            builder.Property(a => a.Descricao).HasColumnType("varchar(80)").IsRequired();
            builder.Property(a => a.Imagem).HasColumnType("varchar(50)");
            builder.Property(a => a.Observacao).HasColumnType("varchar(100)");
            builder.Property(a => a.QuantidadeMinima).HasColumnType("int").IsRequired();
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.UtilizadorId).HasColumnType("int");
            builder.Property(a => a.Valor).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(a => a.Utilizador)
                .WithMany(b => b.Produtos)
                .HasForeignKey(a => a.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

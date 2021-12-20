using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class ProdutoFornecedorMap : IEntityTypeConfiguration<ProdutoFornecedor>
    {
        public void Configure(EntityTypeBuilder<ProdutoFornecedor> builder)
        {
            builder.ToTable("ProdutoFornecedor");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataRegisto).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.FornecedorId).HasColumnType("int");
            builder.Property(a => a.ProdutoId).HasColumnType("int");
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.UtilizadorId).HasColumnType("int");

            builder.HasOne(a => a.Fornecedor)
                .WithMany(b => b.produtos)
                .HasForeignKey(a => a.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Produto)
                .WithMany(b => b.produtos)
                .HasForeignKey(a => a.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Utilizador)
                .WithMany(b => b.produtoFornecedors)
                .HasForeignKey(a => a.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataRegisto).HasColumnType("datetime");
            builder.Property(a => a.DataRegisto).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.DenominacaoFiscal).HasColumnType("varchar(80)").IsRequired();
            builder.Property(a => a.Email).HasColumnType("varchar(150)");
            builder.Property(a => a.Endereco).HasColumnType("varchar(200)");
            builder.Property(a => a.Status).HasColumnType("bit");
            builder.Property(a => a.Telefone).HasColumnType("varchar(20)");
            builder.Property(a => a.UtilizadorId).HasColumnType("int");

            builder.HasOne(a => a.Utilizador)
                .WithMany(b => b.Fornercedores)
                .HasForeignKey(a => a.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

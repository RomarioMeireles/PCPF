using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class PedidoRascunhoMap : IEntityTypeConfiguration<PedidoRascunho>
    {
        public void Configure(EntityTypeBuilder<PedidoRascunho> builder)
        {
            builder.ToTable("PedidoRascunho");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnType("int");
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ProdutoId).HasColumnType("int");
            builder.Property(a => a.Quantidade).HasColumnType("int");
            builder.Property(a => a.Descricao).HasColumnType("varchar(150)");
            builder.Property(a => a.Valor).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(a => a.UserName).HasColumnType("varchar(150)");
            builder.Property(a => a.SessionId).HasColumnType("varchar(100)");
            builder.Property(a => a.Imagem).HasColumnType("varchar(150)");
        }
    }
}

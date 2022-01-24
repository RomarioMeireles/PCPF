using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnType("int");
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataRegisto).HasColumnType("datetime");
            builder.Property(a => a.DataRegisto).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(a => a.DocumentoIdentificacao).HasColumnType("varchar(30)");
            builder.Property(a => a.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(a => a.Nome).HasColumnType("varchar(80)").IsRequired();
            builder.Property(a => a.UserName).HasColumnType("varchar(150)").IsRequired();
            builder.Property(a => a.Status).HasColumnType("bit").IsRequired();
            builder.Property(a => a.Telefone).HasColumnType("varchar(20)");
            
        }
    }
}

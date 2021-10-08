using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCPF.Domain.Model;

namespace PCPF.Infra.Data.Map
{
    public class UtilizadorMap : IEntityTypeConfiguration<Utilizador>
    {
        public void Configure(EntityTypeBuilder<Utilizador> builder)
        {
            builder.ToTable("Utilizador");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnType("nvarchar(128)");
            builder.Property(a => a.Id).HasDefaultValueSql("newid()").ValueGeneratedOnAdd();

            builder.Property(a => a.Password).HasColumnType("varchar(150)");
            builder.Property(a => a.DataRegisto).HasColumnType("datetime");
            builder.Property(a => a.UserName).HasColumnType("varchar(150)").IsRequired();
            builder.Property(a => a.Status).HasColumnType("bit");
        }
    }
}

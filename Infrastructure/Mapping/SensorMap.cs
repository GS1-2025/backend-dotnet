using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class SensorMap : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("SSX_SENSORES");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ID_SENSOR")
                                   .ValueGeneratedOnAdd();


            builder.Property(s => s.ProdutoId)
                   .HasColumnName("SSX_PRODUTOS_ID_PRODUTO");

            builder.Property(s => s.Tipo)
                   .HasColumnName("TIPO")
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(s => s.Descricao)
                   .HasColumnName("DESCRICAO")
                   .HasMaxLength(40);

            builder.Property(s => s.Modelo)
                   .HasColumnName("MODELO")
                   .HasMaxLength(35);

            builder.Property(p => p.Status)
              .HasColumnName("STATUS")
              .HasMaxLength(15)
              .IsRequired();
        }
    }
}

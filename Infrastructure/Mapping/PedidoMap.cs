using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("SSX_PEDIDOS");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ID_PEDIDO")
                                   .ValueGeneratedOnAdd();


            builder.Property(p => p.DataPedido)
                   .HasColumnName("DATA_PEDIDO")
                   .HasColumnType("DATE")
                   .IsRequired();

            builder.Property(p => p.Preco)
                   .HasColumnName("PRECO")
                   .HasColumnType("NUMBER(6,2)")
                   .IsRequired();

            builder.Property(p => p.Status)
                   .HasColumnName("STATUS")
                   .HasMaxLength(15)
                   .IsRequired();
        }
    }
}

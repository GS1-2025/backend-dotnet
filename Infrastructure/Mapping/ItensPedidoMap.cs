using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("SSX_ITENS_PEDIDO");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("ID_ITEM_PEDIDO")
                                       .ValueGeneratedOnAdd();

            builder.Property(i => i.PedidoId)
                   .HasColumnName("SSX_PEDIDOS_ID_PEDIDO")
                   .IsRequired();

            builder.Property(i => i.UsuarioId)
                   .HasColumnName("SSX_USUARIOS_ID_USUARIO")
                   .IsRequired();

            builder.Property(i => i.Quantidade)
                   .HasColumnName("QUANTIDADE")
                   .IsRequired();

            builder.HasOne(i => i.Pedido)
                   .WithOne(p => p.Itens)    
                   .HasForeignKey<ItensPedido>(i => i.PedidoId)
                   .IsRequired();

            builder.HasOne(i => i.Usuario)
                   .WithMany(u => u.ItensPedido)
                   .HasForeignKey(i => i.UsuarioId);

            builder.HasMany(i => i.Produtos)
                   .WithOne()
                   .HasForeignKey(p => p.ItemPedidoId);
        }
    }
}

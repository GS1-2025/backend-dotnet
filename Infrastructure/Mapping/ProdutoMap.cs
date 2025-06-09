using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("SSX_PRODUTOS");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ID_PRODUTO")
                                   .ValueGeneratedOnAdd();


            builder.Property(p => p.Nome)
                   .HasColumnName("NOME")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(p => p.Descricao)
                   .HasColumnName("DESCRICAO")
                   .HasMaxLength(80);

            builder.Property(p => p.PrecoUnitario)
                   .HasColumnName("PRECO_UNITARIO")
                   .HasColumnType("NUMBER");
            
            builder.Property(p => p.ItemPedidoId)
                .HasColumnName("SSX_IP_ID_ITEM_PEDIDO");

            
        }
    }
}

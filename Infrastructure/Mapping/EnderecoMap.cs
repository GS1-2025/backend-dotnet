using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("SSX_ENDERECOS");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID_ENDERECO")
                .ValueGeneratedOnAdd();


            builder.Property(e => e.UsuarioId)
                      .HasColumnName("SSX_USUARIOS_ID_USUARIO");

            builder.HasOne(e => e.Usuario)         
                   .WithMany(u => u.Enderecos)
                   .HasForeignKey(e => e.UsuarioId)
                   .HasConstraintName("SSX_USUARIOS_ID_USUARIO");
        
          


            builder.Property(e => e.Cep)
                   .HasColumnName("CEP")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasColumnName("ESTADO")
                   .HasMaxLength(2)
                   .IsRequired();

            builder.Property(e => e.Cidade)
                   .HasColumnName("CIDADE")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Bairro)
                   .HasColumnName("BAIRRO")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Rua)
                   .HasColumnName("RUA")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}

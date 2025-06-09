using gs_sensolux.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("SSX_USUARIOS");


            builder.Property(u => u.Id)
                     .HasColumnName("ID_USUARIO")
                     .ValueGeneratedOnAdd();

            builder.Property(u => u.Nome)
                   .HasColumnName("NOME")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Senha)
                   .HasColumnName("SENHA")
                   .HasMaxLength(100) 
                   .IsRequired();

            builder.Property(u => u.Role)
                   .HasColumnName("ROLE")
                   .HasMaxLength(50) 
                   .IsRequired();

            builder.Property(u => u.EmailId)
                   .HasColumnName("SSX_EMAILS_ID_EMAIL")
                   .IsRequired();

            builder.HasOne(u => u.Email)
                   .WithOne()
                   .HasForeignKey<Usuario>(u => u.EmailId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Enderecos)
                   .WithOne(e => e.Usuario)
                   .HasForeignKey(e => e.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ItensPedido)
                   .WithOne(i => i.Usuario)
                   .HasForeignKey(i => i.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

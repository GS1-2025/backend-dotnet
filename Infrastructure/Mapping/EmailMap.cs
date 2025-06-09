using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using gs_sensolux.Domain.Entity;

namespace gs_sensolux.Infrastructure.Mapping
{
    public class EmailMap : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("SSX_EMAILS");

             builder.Property(e => e.Id)
                   .HasColumnName("ID_EMAIL")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Endereco)
                   .HasColumnName("ENDERECO")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Ativo)
                   .HasColumnName("ATIVO")
                   .IsRequired();
        }
    }
}

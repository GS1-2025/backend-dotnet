using Microsoft.EntityFrameworkCore;
using gs_sensolux.Domain.Entity;
using gs_sensolux.Infrastructure.Mapping;

namespace gs_sensolux.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedido { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Conversão automática de bool -> int (0 ou 1), para Oracle
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var boolProps = entityType.ClrType
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?));

                foreach (var prop in boolProps)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(prop.Name)
                        .HasConversion<int>();
                }
            }
            modelBuilder.ApplyConfiguration(new UsuarioMap()); // <- ISSO É ESSENCIAL

            modelBuilder.Entity<Usuario>()
                .ToTable("SSX_USUARIOS", t => t.ExcludeFromMigrations()); // <- MANTÉM ISSO SE NÃO QUISER GERAR

            modelBuilder.Entity<Email>()
                .ToTable("SSX_EMAILS", t => t.ExcludeFromMigrations());

            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new ItensPedidoMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new SensorMap());
        }
    }
    }

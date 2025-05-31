using Entity.models;
using Microsoft.EntityFrameworkCore;


namespace Entity.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Factura
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.FacturaId);
                entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(20);
                entity.Property(e => e.NombreCliente).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DocumentoCliente).HasMaxLength(50);
                entity.Property(e => e.Observaciones).HasMaxLength(500);
                entity.Property(e => e.Estado).HasMaxLength(20).HasDefaultValue("Pendiente");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Impuesto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");

                // Índice único para número de factura
                entity.HasIndex(e => e.NumeroFactura).IsUnique();
            });

            // Configuración de DetalleFactura
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.DetalleFacturaId);
                entity.Property(e => e.CodigoProducto).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DescripcionProducto).IsRequired().HasMaxLength(300);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Descuento).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

                // Relación con Factura
                entity.HasOne(d => d.Factura)
                      .WithMany(p => p.DetallesFactura)
                      .HasForeignKey(d => d.FacturaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
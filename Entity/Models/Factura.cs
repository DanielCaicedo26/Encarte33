
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroFactura { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCliente { get; set; }

        [StringLength(50)]
        public string DocumentoCliente { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Impuesto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";

        // Relación con detalle
        public virtual ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
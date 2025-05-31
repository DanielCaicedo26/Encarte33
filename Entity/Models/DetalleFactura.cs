using Entity.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.models
{
    public class DetalleFactura
    {
        [Key]
        public int DetalleFacturaId { get; set; }

        [Required]
        public int FacturaId { get; set; }

        [Required]
        [StringLength(100)]
        public string CodigoProducto { get; set; }

        [Required]
        [StringLength(300)]
        public string DescripcionProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        // Clave foránea
        [ForeignKey("FacturaId")]
        public virtual Factura Factura { get; set; }
    }
}
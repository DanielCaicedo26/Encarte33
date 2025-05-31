using System.ComponentModel.DataAnnotations;

namespace Entity.Dto
{
    public class DetalleFacturaDto
    {
        public int DetalleFacturaId { get; set; }
        public int FacturaId { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CrearDetalleFacturaDto
    {
        [Required(ErrorMessage = "El código del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El código no puede exceder 100 caracteres")]
        public string CodigoProducto { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria")]
        [StringLength(300, ErrorMessage = "La descripción no puede exceder 300 caracteres")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser negativo")]
        public decimal Descuento { get; set; } = 0;
    }

    public class ActualizarDetalleFacturaDto
    {
        public int DetalleFacturaId { get; set; }

        [Required(ErrorMessage = "El código del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El código no puede exceder 100 caracteres")]
        public string CodigoProducto { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria")]
        [StringLength(300, ErrorMessage = "La descripción no puede exceder 300 caracteres")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser negativo")]
        public decimal Descuento { get; set; } = 0;
    }
}
using Entity.Dto;
using System.ComponentModel.DataAnnotations;

namespace Entity.Dto

{
    public class FacturaDto
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; }
        public string NombreCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public List<DetalleFacturaDto> DetallesFactura { get; set; } = new List<DetalleFacturaDto>();
    }

    public class CrearFacturaDto
    {
        [Required(ErrorMessage = "El número de factura es obligatorio")]
        [StringLength(20, ErrorMessage = "El número de factura no puede exceder 20 caracteres")]
        public string NumeroFactura { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre del cliente no puede exceder 200 caracteres")]
        public string NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "El documento no puede exceder 50 caracteres")]
        public string DocumentoCliente { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria")]
        public DateTime FechaEmision { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        public List<CrearDetalleFacturaDto> DetallesFactura { get; set; } = new List<CrearDetalleFacturaDto>();
    }

    public class ActualizarFacturaDto
    {
        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre del cliente no puede exceder 200 caracteres")]
        public string NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "El documento no puede exceder 50 caracteres")]
        public string DocumentoCliente { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria")]
        public DateTime FechaEmision { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        public List<ActualizarDetalleFacturaDto> DetallesFactura { get; set; } = new List<ActualizarDetalleFacturaDto>();
    }
}

using Data;
using Entity.Dto;
using Entity.models;


namespace Business
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IDetalleFacturaRepository _detalleRepository;
        private readonly IMapper _mapper;

        public FacturaService(
            IFacturaRepository facturaRepository,
            IDetalleFacturaRepository detalleRepository,
            IMapper mapper)
        {
            _facturaRepository = facturaRepository;
            _detalleRepository = detalleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacturaDto>> ObtenerTodasLasFacturasAsync()
        {
            var facturas = await _facturaRepository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<FacturaDto>>(facturas);
        }

        public async Task<FacturaDto> ObtenerFacturaPorIdAsync(int id)
        {
            var factura = await _facturaRepository.ObtenerPorIdAsync(id);
            if (factura == null)
                throw new KeyNotFoundException($"No se encontró la factura con ID {id}");

            return _mapper.Map<FacturaDto>(factura);
        }

        public async Task<FacturaDto> CrearFacturaAsync(CrearFacturaDto crearFacturaDto)
        {
            // Validar que no exista el número de factura
            if (await _facturaRepository.ExisteNumeroFacturaAsync(crearFacturaDto.NumeroFactura))
                throw new InvalidOperationException($"Ya existe una factura con el número {crearFacturaDto.NumeroFactura}");

            var factura = _mapper.Map<Factura>(crearFacturaDto);

            // Calcular totales
            CalcularTotalesFactura(factura);

            var facturaCreada = await _facturaRepository.CrearAsync(factura);
            return _mapper.Map<FacturaDto>(facturaCreada);
        }

        public async Task<FacturaDto> ActualizarFacturaAsync(int id, ActualizarFacturaDto actualizarFacturaDto)
        {
            var facturaExistente = await _facturaRepository.ObtenerPorIdAsync(id);
            if (facturaExistente == null)
                throw new KeyNotFoundException($"No se encontró la factura con ID {id}");

            _mapper.Map(actualizarFacturaDto, facturaExistente);

            // Calcular totales
            CalcularTotalesFactura(facturaExistente);

            var facturaActualizada = await _facturaRepository.ActualizarAsync(facturaExistente);
            return _mapper.Map<FacturaDto>(facturaActualizada);
        }

        public async Task<bool> EliminarFacturaAsync(int id)
        {
            if (!await _facturaRepository.ExisteAsync(id))
                throw new KeyNotFoundException($"No se encontró la factura con ID {id}");

            return await _facturaRepository.EliminarAsync(id);
        }

        private void CalcularTotalesFactura(Factura factura)
        {
            foreach (var detalle in factura.DetallesFactura)
            {
                detalle.Subtotal = (detalle.Cantidad * detalle.PrecioUnitario) - detalle.Descuento;
            }

            factura.Subtotal = factura.DetallesFactura.Sum(d => d.Subtotal);
            factura.Impuesto = factura.Subtotal * 0.19m; // IVA 19%
            factura.Total = factura.Subtotal + factura.Impuesto;
        }
    }
}

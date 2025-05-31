using AutoMapper;
using Business;
using Data;
using Entity.Dto;
using Entity.models;

namespace Business
{
    public class DetalleFacturaService : IDetalleFacturaService
    {
        private readonly IDetalleFacturaRepository _detalleRepository;
        private readonly IFacturaRepository _facturaRepository;
        private readonly IMapper _mapper;

        public DetalleFacturaService(
            IDetalleFacturaRepository detalleRepository,
            IFacturaRepository facturaRepository,
            IMapper mapper)
        {
            _detalleRepository = detalleRepository;
            _facturaRepository = facturaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetalleFacturaDto>> ObtenerDetallesPorFacturaAsync(int facturaId)
        {
            var detalles = await _detalleRepository.ObtenerPorFacturaIdAsync(facturaId);
            return _mapper.Map<IEnumerable<DetalleFacturaDto>>(detalles);
        }

        public async Task<DetalleFacturaDto> ObtenerDetallePorIdAsync(int id)
        {
            var detalle = await _detalleRepository.ObtenerPorIdAsync(id);
            if (detalle == null)
                throw new KeyNotFoundException($"No se encontró el detalle con ID {id}");

            return _mapper.Map<DetalleFacturaDto>(detalle);
        }

        public async Task<DetalleFacturaDto> CrearDetalleAsync(int facturaId, CrearDetalleFacturaDto crearDetalleDto)
        {
            // Verificar que existe la factura
            if (!await _facturaRepository.ExisteAsync(facturaId))
                throw new KeyNotFoundException($"No se encontró la factura con ID {facturaId}");

            var detalle = _mapper.Map<DetalleFactura>(crearDetalleDto);
            detalle.FacturaId = facturaId;

            // Calcular subtotal
            detalle.Subtotal = (detalle.Cantidad * detalle.PrecioUnitario) - detalle.Descuento;

            var detalleCreado = await _detalleRepository.CrearAsync(detalle);
            return _mapper.Map<DetalleFacturaDto>(detalleCreado);
        }

        public async Task<DetalleFacturaDto> ActualizarDetalleAsync(int id, ActualizarDetalleFacturaDto actualizarDetalleDto)
        {
            var detalleExistente = await _detalleRepository.ObtenerPorIdAsync(id);
            if (detalleExistente == null)
                throw new KeyNotFoundException($"No se encontró el detalle con ID {id}");

            _mapper.Map(actualizarDetalleDto, detalleExistente);

            // Recalcular subtotal
            detalleExistente.Subtotal = (detalleExistente.Cantidad * detalleExistente.PrecioUnitario) - detalleExistente.Descuento;

            var detalleActualizado = await _detalleRepository.ActualizarAsync(detalleExistente);
            return _mapper.Map<DetalleFacturaDto>(detalleActualizado);
        }

        public async Task<bool> EliminarDetalleAsync(int id)
        {
            if (!await _detalleRepository.ExisteAsync(id))
                throw new KeyNotFoundException($"No se encontró el detalle con ID {id}");

            return await _detalleRepository.EliminarAsync(id);
        }
    }
}
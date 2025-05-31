using Entity.Dto;


namespace Business
{
    public interface IDetalleFacturaService
    {
        Task<IEnumerable<DetalleFacturaDto>> ObtenerDetallesPorFacturaAsync(int facturaId);
        Task<DetalleFacturaDto> ObtenerDetallePorIdAsync(int id);
        Task<DetalleFacturaDto> CrearDetalleAsync(int facturaId, CrearDetalleFacturaDto crearDetalleDto);
        Task<DetalleFacturaDto> ActualizarDetalleAsync(int id, ActualizarDetalleFacturaDto actualizarDetalleDto);
        Task<bool> EliminarDetalleAsync(int id);
    }
}
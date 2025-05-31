using Entity.models;


namespace Data
{
    public interface IDetalleFacturaRepository
    {
        Task<IEnumerable<DetalleFactura>> ObtenerPorFacturaIdAsync(int facturaId);
        Task<DetalleFactura> ObtenerPorIdAsync(int id);
        Task<DetalleFactura> CrearAsync(DetalleFactura detalle);
        Task<DetalleFactura> ActualizarAsync(DetalleFactura detalle);
        Task<bool> EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}

using Entity.Dto;



namespace Business
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaDto>> ObtenerTodasLasFacturasAsync();
        Task<FacturaDto> ObtenerFacturaPorIdAsync(int id);
        Task<FacturaDto> CrearFacturaAsync(CrearFacturaDto crearFacturaDto);
        Task<FacturaDto> ActualizarFacturaAsync(int id, ActualizarFacturaDto actualizarFacturaDto);
        Task<bool> EliminarFacturaAsync(int id);
    }
}

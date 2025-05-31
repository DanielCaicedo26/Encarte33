using Entity.models;


namespace Data
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> ObtenerTodasAsync();
        Task<Factura> ObtenerPorIdAsync(int id);
        Task<Factura> CrearAsync(Factura factura);
        Task<Factura> ActualizarAsync(Factura factura);
        Task<bool> EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteNumeroFacturaAsync(string numeroFactura);
    }
}

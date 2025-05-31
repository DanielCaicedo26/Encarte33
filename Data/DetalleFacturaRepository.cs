
using Entity.aplication;
using Entity.models;    
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public DetalleFacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleFactura>> ObtenerPorFacturaIdAsync(int facturaId)
        {
            return await _context.DetallesFactura
                .Where(d => d.FacturaId == facturaId)
                .ToListAsync();
        }

        public async Task<DetalleFactura> ObtenerPorIdAsync(int id)
        {
            return await _context.DetallesFactura
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(d => d.DetalleFacturaId == id);
        }

        public async Task<DetalleFactura> CrearAsync(DetalleFactura detalle)
        {
            _context.DetallesFactura.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<DetalleFactura> ActualizarAsync(DetalleFactura detalle)
        {
            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var detalle = await _context.DetallesFactura.FindAsync(id);
            if (detalle == null)
                return false;

            _context.DetallesFactura.Remove(detalle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.DetallesFactura.AnyAsync(d => d.DetalleFacturaId == id);
        }
    }
}

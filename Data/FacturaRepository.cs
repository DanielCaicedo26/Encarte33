using Entity.aplication;
using Entity.models;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> ObtenerTodasAsync()
        {
            return await _context.Facturas
                .Include(f => f.DetallesFactura)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<Factura> ObtenerPorIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.DetallesFactura)
                .FirstOrDefaultAsync(f => f.FacturaId == id);
        }

        public async Task<Factura> CrearAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<Factura> ActualizarAsync(Factura factura)
        {
            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return false;

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Facturas.AnyAsync(f => f.FacturaId == id);
        }

        public async Task<bool> ExisteNumeroFacturaAsync(string numeroFactura)
        {
            return await _context.Facturas.AnyAsync(f => f.NumeroFactura == numeroFactura);
        }
    }
}
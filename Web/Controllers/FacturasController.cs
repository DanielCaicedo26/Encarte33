using Business;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // GET: api/facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDto>>> ObtenerFacturas()
        {
            try
            {
                var facturas = await _facturaService.ObtenerTodasLasFacturasAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // GET: api/facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDto>> ObtenerFactura(int id)
        {
            try
            {
                var factura = await _facturaService.ObtenerFacturaPorIdAsync(id);
                return Ok(factura);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // POST: api/facturas
        [HttpPost]
        public async Task<ActionResult<FacturaDto>> CrearFactura(CrearFacturaDto crearFacturaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var factura = await _facturaService.CrearFacturaAsync(crearFacturaDto);
                return CreatedAtAction(nameof(ObtenerFactura), new { id = factura.FacturaId }, factura);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // PUT: api/facturas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FacturaDto>> ActualizarFactura(int id, ActualizarFacturaDto actualizarFacturaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var factura = await _facturaService.ActualizarFacturaAsync(id, actualizarFacturaDto);
                return Ok(factura);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // DELETE: api/facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFactura(int id)
        {
            try
            {
                await _facturaService.EliminarFacturaAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }
    }
}

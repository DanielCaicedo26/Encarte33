using Business;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [ApiController]
    [Route("api/facturas/{facturaId}/detalles")]
    public class DetallesFacturaController : ControllerBase
    {
        private readonly IDetalleFacturaService _detalleService;

        public DetallesFacturaController(IDetalleFacturaService detalleService)
        {
            _detalleService = detalleService;
        }

        // GET: api/facturas/5/detalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDto>>> ObtenerDetallesPorFactura(int facturaId)
        {
            try
            {
                var detalles = await _detalleService.ObtenerDetallesPorFacturaAsync(facturaId);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // GET: api/detalles/5
        [HttpGet]
        [Route("/api/detalles/{id}")]
        public async Task<ActionResult<DetalleFacturaDto>> ObtenerDetalle(int id)
        {
            try
            {
                var detalle = await _detalleService.ObtenerDetallePorIdAsync(id);
                return Ok(detalle);
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

        // POST: api/facturas/5/detalles
        [HttpPost]
        public async Task<ActionResult<DetalleFacturaDto>> CrearDetalle(int facturaId, CrearDetalleFacturaDto crearDetalleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var detalle = await _detalleService.CrearDetalleAsync(facturaId, crearDetalleDto);
                return CreatedAtAction(nameof(ObtenerDetalle), new { id = detalle.DetalleFacturaId }, detalle);
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

        // PUT: api/detalles/5
        [HttpPut]
        [Route("/api/detalles/{id}")]
        public async Task<ActionResult<DetalleFacturaDto>> ActualizarDetalle(int id, ActualizarDetalleFacturaDto actualizarDetalleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var detalle = await _detalleService.ActualizarDetalleAsync(id, actualizarDetalleDto);
                return Ok(detalle);
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

        // DELETE: api/detalles/5
        [HttpDelete]
        [Route("/api/detalles/{id}")]
        public async Task<IActionResult> EliminarDetalle(int id)
        {
            try
            {
                await _detalleService.EliminarDetalleAsync(id);
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
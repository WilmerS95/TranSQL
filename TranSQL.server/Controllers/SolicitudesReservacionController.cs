using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesReservacionController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public SolicitudesReservacionController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<SolicitudesReservacionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudReservacion>>> GetSolicitudesReservacion()
        {
            return await _context.SolicitudesReservacion.ToListAsync();
        }

        // GET api/<SolicitudesReservacionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudReservacion>> GetSolicitudReservacion(int id)
        {
            var solicitudReservacion = await _context.SolicitudesReservacion.FindAsync(id);
            if (solicitudReservacion == null)
            {
                return NotFound();
            }

            //return Ok(solicitudReservacion);
            return solicitudReservacion;
        }

        // POST api/<SolicitudesReservacionController>
        [HttpPost]
        public async Task<ActionResult<SolicitudReservacion>> PostSolicitudReservacion(SolicitudReservacion solicitudReservacion)
        {
            _context.SolicitudesReservacion.Add(solicitudReservacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitudReservacion", new { id = solicitudReservacion.IdSolicitud }, solicitudReservacion);

        }

        // PUT api/<SolicitudesReservacionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudReservacion(int id, SolicitudReservacion solicitudReservacion)
        {
            if (id != solicitudReservacion.IdSolicitud)
            {
                return BadRequest();
            }
            _context.Entry(solicitudReservacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<SolicitudesReservacionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudReservacion(int id)
        {
            var solicitudReservacion = await _context.SolicitudesReservacion.FindAsync(id);
            if (solicitudReservacion == null)
            {
                return NotFound();
            }
            _context.SolicitudesReservacion.Remove(solicitudReservacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

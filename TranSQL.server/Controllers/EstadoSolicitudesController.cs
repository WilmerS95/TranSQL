using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoSolicitudesController : ControllerBase
    {

        private readonly TranSQLDbContext _context;

        public EstadoSolicitudesController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<EstadoSolicitudesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoSolicitud>>> GetEstadoSolicitudes()
        {
            return await _context.EstadosSolicitudes.ToListAsync();
        }

        // GET api/<EstadoSolicitudesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoSolicitud>> GetEstadoSolicitud(int id)
        {
            var estadoSolicitud = await _context.EstadosSolicitudes.FindAsync(id);
            if (estadoSolicitud == null)
            {
                return NotFound();
            }

            //return Ok(estadoSolicitud);
            return estadoSolicitud;
        }

        // POST api/<EstadoSolicitudesController>
        [HttpPost]
        public async Task<ActionResult<EstadoSolicitud>> PostEstadoSolicitud(EstadoSolicitud estadoSolicitud)
        {
            _context.EstadosSolicitudes.Add(estadoSolicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoSolicitud", new { id = estadoSolicitud.IdEstadoSolicitud }, estadoSolicitud);

        }

        // PUT api/<EstadoSolicitudesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoSolicitud(int id, EstadoSolicitud estadoSolicitud)
        {
            if (id != estadoSolicitud.IdEstadoSolicitud)
            {
                return BadRequest();
            }
            _context.Entry(estadoSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<EstadoSolicitudesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoSolicitud(int id)
        {
            var estadoSolicitud = await _context.EstadosSolicitudes.FindAsync(id);
            if (estadoSolicitud == null)
            {
                return NotFound();
            }
            _context.EstadosSolicitudes.Remove(estadoSolicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

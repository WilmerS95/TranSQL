using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public ReservacionesController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<ReservacionesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservaciones()
        {
            return await _context.Reservaciones.ToListAsync();
        }

        // GET api/<ReservacionesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservacion>> GetReservacion(int id)
        {
            var reservacion = await _context.Reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            //return Ok(reservacion);
            return reservacion;
        }

        // POST api/<ReservacionesController>
        [HttpPost]
        public async Task<ActionResult<Reservacion>> PostReservacion(Reservacion reservacion)
        {
            _context.Reservaciones.Add(reservacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservacion", new { id = reservacion.IdReservacion }, reservacion);

        }

        // PUT api/<ReservacionesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacion(int id, Reservacion reservacion)
        {
            if (id != reservacion.IdReservacion)
            {
                return BadRequest();
            }
            _context.Entry(reservacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ReservacionesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacion(int id)
        {
            var reservacion = await _context.Reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }
            _context.Reservaciones.Remove(reservacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

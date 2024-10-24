using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoVehiculosController : ControllerBase
    {

        private readonly TranSQLDbContext _context;

        public EstadoVehiculosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<EstadoVehiculosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoVehiculo>>> GetEstadoVehiculos()
        {
            return await _context.EstadosVehiculo.ToListAsync();
        }

        // GET api/<EstadoVehiculosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoVehiculo>> GetEstadoVehiculo(int id)
        {
            var estadoVehiculo = await _context.EstadosVehiculo.FindAsync(id);
            if (estadoVehiculo == null)
            {
                return NotFound();
            }

            //return Ok(estadoVehiculo);
            return estadoVehiculo;
        }

        // POST api/<EstadoVehiculosController>
        [HttpPost]
        public async Task<ActionResult<EstadoVehiculo>> PostEstadoVehiculo(EstadoVehiculo estadoVehiculo)
        {
            _context.EstadosVehiculo.Add(estadoVehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoVehiculo", new { id = estadoVehiculo.IdEstadoVehiculo }, estadoVehiculo);

        }

        // PUT api/<EstadoVehiculosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoVehiculo(int id, EstadoVehiculo estadoVehiculo)
        {
            if (id != estadoVehiculo.IdEstadoVehiculo)
            {
                return BadRequest();
            }
            _context.Entry(estadoVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<EstadoVehiculosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoVehiculo(int id)
        {
            var estadoVehiculo = await _context.EstadosVehiculo.FindAsync(id);
            if (estadoVehiculo == null)
            {
                return NotFound();
            }
            _context.EstadosVehiculo.Remove(estadoVehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionVehiculosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public InspeccionVehiculosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<InspeccionVehiculosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspeccionVehiculo>>> GetInspeccionVehiculos()
        {
            return await _context.InspeccionVehiculos.ToListAsync();
        }

        // GET api/<InspeccionVehiculosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InspeccionVehiculo>> GetInspeccionVehiculo(int id)
        {
            var inspeccionVehiculo = await _context.InspeccionVehiculos.FindAsync(id);
            if (inspeccionVehiculo == null)
            {
                return NotFound();
            }

            //return Ok(inspeccionVehiculo);
            return inspeccionVehiculo;
        }

        // POST api/<InspeccionVehiculosController>
        [HttpPost]
        public async Task<ActionResult<InspeccionVehiculo>> PostInspeccionVehiculo(InspeccionVehiculo inspeccionVehiculo)
        {
            _context.InspeccionVehiculos.Add(inspeccionVehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspeccionVehiculo", new { id = inspeccionVehiculo.IdInspeccion }, inspeccionVehiculo);

        }

        // PUT api/<InspeccionVehiculosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspeccionVehiculo(int id, InspeccionVehiculo inspeccionVehiculo)
        {
            if (id != inspeccionVehiculo.IdInspeccion)
            {
                return BadRequest();
            }
            _context.Entry(inspeccionVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<InspeccionVehiculosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspeccionVehiculo(int id)
        {
            var inspeccionVehiculo = await _context.InspeccionVehiculos.FindAsync(id);
            if (inspeccionVehiculo == null)
            {
                return NotFound();
            }
            _context.InspeccionVehiculos.Remove(inspeccionVehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

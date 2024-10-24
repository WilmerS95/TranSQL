using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVehiculosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public TipoVehiculosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<TipoVehiculosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVehiculo>>> GetTipoVehiculos()
        {
            return await _context.TipoVehiculos.ToListAsync();
        }

        // GET api/<TipoVehiculosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVehiculo>> GetTipoVehiculo(int id)
        {
            var tipoVehiculos = await _context.TipoVehiculos.FindAsync(id);
            if (tipoVehiculos == null)
            {
                return NotFound();
            }

            //return Ok(tipoVehiculos);
            return tipoVehiculos;
        }

        // POST api/<TipoVehiculosController>
        [HttpPost]
        public async Task<ActionResult<TipoVehiculo>> PostTipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            _context.TipoVehiculos.Add(tipoVehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoVehiculo", new { id = tipoVehiculo.IdTipoVehiculo }, tipoVehiculo);

        }

        // PUT api/<TipoVehiculosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoVehiculo(int id, TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.IdTipoVehiculo)
            {
                return BadRequest();
            }
            _context.Entry(tipoVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<TipoVehiculosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoVehiculo(int id)
        {
            var tipoVehiculo = await _context.TipoVehiculos.FindAsync(id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            _context.TipoVehiculos.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

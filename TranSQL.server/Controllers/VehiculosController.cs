using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public VehiculosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<VehiculosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        // GET api/<VehiculosController>/5
        [HttpGet("{placa}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(string placa)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(placa);
            if (vehiculo == null)
            {
                return NotFound();
            }

            //return Ok(vehiculo);
            return vehiculo;
        }

        // POST api/<VehiculosController>
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehiculo", new { placa = vehiculo.Placa }, vehiculo);

        }

        // PUT api/<VehiculosController>/5
        [HttpPut("{placa}")]
        public async Task<IActionResult> PutVehiculo(string placa, Vehiculo vehiculo)
        {
            if (placa != vehiculo.Placa)
            {
                return BadRequest();
            }
            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<VehiculosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(string placa)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(placa);
            if (vehiculo == null)
            {
                return NotFound();
            }
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

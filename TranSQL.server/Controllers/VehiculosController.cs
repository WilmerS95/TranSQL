using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranSQL.shared.models;

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
        public async Task<ActionResult<IEnumerable<VehiculoDto>>> GetVehiculos()
        {
            // Consulta que incluye TipoVehiculo y EstadoVehiculo
            var vehiculos = await _context.Vehiculos
                .Include(v => v.TipoVehiculo)
                .Include(v => v.EstadoVehiculo)
                .ToListAsync();

            // Mapea los vehículos a DTOs
            var vehiculosDto = vehiculos.Select(v => new VehiculoDto
            {
                Placa = v.Placa,
                Modelo = v.Modelo,
                OdometroInicial = v.OdometroInicial,
                OdometroFinal = v.OdometroFinal,
                IdTipoVehiculo = v.IdTipoVehiculo,
                IdEstadoVehiculo = v.IdEstadoVehiculo,
                TipoVehiculo = v.TipoVehiculo?.NombreTipoVehiculo, // Suponiendo que TipoVehiculo tiene una propiedad Nombre
                EstadoVehiculo = v.EstadoVehiculo?.NombreEstadoVehiculo // Suponiendo que EstadoVehiculo tiene una propiedad Nombre
            });

            return Ok(vehiculosDto);
        }

        // GET api/<VehiculosController>/5
        [HttpGet("{placa}")]
        public async Task<ActionResult<VehiculoDto>> GetVehiculo(string placa)
        {
            var vehiculo = await _context.Vehiculos
                .Include(v => v.TipoVehiculo) // Incluir TipoVehiculo
                .Include(v => v.EstadoVehiculo) // Incluir EstadoVehiculo
                .FirstOrDefaultAsync(v => v.Placa == placa);

            if (vehiculo == null)
            {
                return NotFound();
            }

            // Mapea el vehículo a un DTO
            var vehiculoDto = new VehiculoDto
            {
                Placa = vehiculo.Placa,
                Modelo = vehiculo.Modelo,
                OdometroInicial = vehiculo.OdometroInicial,
                OdometroFinal = vehiculo.OdometroFinal,
                IdTipoVehiculo = vehiculo.IdTipoVehiculo,
                IdEstadoVehiculo = vehiculo.IdEstadoVehiculo,
                TipoVehiculo = vehiculo.TipoVehiculo?.NombreTipoVehiculo,
                EstadoVehiculo = vehiculo.EstadoVehiculo?.NombreEstadoVehiculo
            };

            return Ok(vehiculoDto);
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
        [HttpDelete("{placa}")]
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

    // DTO para Vehiculo
    public class VehiculoDto
    {
        public string Placa { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public int IdTipoVehiculo { get; set; }
        //public TipoVehiculo TipoVehiculo { get; set; }
        //public EstadoVehiculo EstadoVehiculo { get; set; }
        public int IdEstadoVehiculo { get; set; }
        public string? TipoVehiculo { get; set; } // Suponiendo que solo necesitas el nombre
        public string? EstadoVehiculo { get; set; } // Suponiendo que solo necesitas el nombre
    }
}

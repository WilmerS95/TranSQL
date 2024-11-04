using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranSQL.shared.models;
using TranSQL.shared.DTO;

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

        [HttpGet("management")]
        public async Task<ActionResult<IEnumerable<VehiculoManagementDTO>>> GetVehiculoManagementInfo()
        {
            var vehiculos = await _context.Vehiculos
                .Include(v => v.EstadoVehiculo)
                .Include(v => v.Asignaciones)
                .ThenInclude(a => a.SolicitudReservacion)
                .ThenInclude(s => s.Colaborador)
                .Select(v => new VehiculoManagementDTO
                {
                    Placa = v.Placa,
                    Modelo = v.Modelo,
                    EstadoVehiculo = v.EstadoVehiculo.NombreEstadoVehiculo,
                    Asignacion = v.Asignaciones.Select(a => new AsignacionInfoDTO
                    {
                        Fecha = a.SolicitudReservacion.Fecha,
                        Colaborador = $"{a.SolicitudReservacion.Colaborador.PrimerNombre} {a.SolicitudReservacion.Colaborador.PrimerApellido}",
                        EstadoSolicitud = a.EstadoSolicitud.NombreEstadoSolicitud
                    }).FirstOrDefault()
                })
                .ToListAsync();

            return Ok(vehiculos);
        }

        [HttpPut("{placa}/estado")]
        public async Task<IActionResult> UpdateVehicleStatus(string placa, [FromBody] UpdateVehicleStatusDTO updateDto)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(placa);
            if (vehiculo == null) return NotFound();

            vehiculo.IdEstadoVehiculo = updateDto.NuevoEstado;
            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<VehiculoManagementDTO>>> GetVehiculosPorEstado(int estadoId)
        {
            var vehiculos = await _context.Vehiculos
                .Include(v => v.EstadoVehiculo)
                .Include(v => v.Asignaciones) // Include relacionado si necesitas detalles de asignación
                .Where(v => v.IdEstadoVehiculo == estadoId)
                .Select(v => new VehiculoManagementDTO
                {
                    Placa = v.Placa,
                    Modelo = v.Modelo,
                    EstadoVehiculo = v.EstadoVehiculo.NombreEstadoVehiculo, // Mapeo a nombre de estado
                    Asignacion = v.Asignaciones.Select(a => new AsignacionInfoDTO
                    {
                        // Propiedades de AsignacionInfoDTO que necesites
                    }).FirstOrDefault()
                })
                .ToListAsync();

            return Ok(vehiculos);
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

        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculosDisponibles()
        {
            var disponibles = await _context.Vehiculos
                .Where(v => v.IdEstadoVehiculo == 1) // 1 = Disponible
                .ToListAsync();
            return Ok(disponibles);
        }

        // PUT: api/vehiculos/asignar-vehiculos/{idSolicitud}
        [HttpPut("asignar-vehiculos/{idSolicitud}")]
        public async Task<IActionResult> AsignarVehiculos(int idSolicitud, [FromBody] List<string> placasSeleccionadas)
        {
            var solicitud = await _context.SolicitudesReservacion.FindAsync(idSolicitud);
            if (solicitud == null)
            {
                return NotFound("Solicitud no encontrada");
            }

            var vehiculos = await _context.Vehiculos
                .Where(v => placasSeleccionadas.Contains(v.Placa) && v.IdEstadoVehiculo == 1)
                .ToListAsync();

            foreach (var vehiculo in vehiculos)
            {
                vehiculo.IdEstadoVehiculo = 2;
            }

            await _context.SaveChangesAsync();
            return Ok("Vehículos asignados y estado actualizado.");
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
        public async Task<ActionResult<Vehiculo>> PostVehiculo(CreateVehiculoDTO vehiculoDto)
        {
            // Mapea el DTO al modelo Vehiculo
            var vehiculo = new Vehiculo
            {
                Placa = vehiculoDto.Placa,
                Modelo = vehiculoDto.Modelo,
                OdometroInicial = vehiculoDto.OdometroInicial,
                OdometroFinal = vehiculoDto.OdometroFinal,
                IdTipoVehiculo = vehiculoDto.IdTipoVehiculo,
                IdEstadoVehiculo = vehiculoDto.IdEstadoVehiculo
            };

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

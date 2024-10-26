using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;
using TranSQL.shared.Services;
using TranSQL.shared.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesReservacionController : ControllerBase
    {
        private readonly TranSQLDbContext _context;
        private readonly IEmailService _emailService;

        public SolicitudesReservacionController(TranSQLDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/<SolicitudesReservacionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudReservacion>>> GetSolicitudesReservacion()
        {
            var solicitudes = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.EstadoSolicitud)
                .ToListAsync();

            return Ok(solicitudes);
        }

        [HttpGet("colaborador/{idColaborador}")]
        public async Task<ActionResult<IEnumerable<SolicitudReservacion>>> GetSolicitudesPorColaborador(int idColaborador)
        {
            var solicitudes = await _context.SolicitudesReservacion
                .Where(s => s.IdColaborador == idColaborador)
                .Include(s => s.Colaborador)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.EstadoSolicitud)
                .ToListAsync();

            if (solicitudes == null || !solicitudes.Any())
            {
                return NotFound($"No se encontraron solicitudes para el colaborador con Id {idColaborador}.");
            }

            return Ok(solicitudes);
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

        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<SolicitudReservacion>>> GetSolicitudesPendientes()
        {
            var solicitudesPendientes = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador)
                .ThenInclude(c => c.Departamento)  // Incluye Departamento
                .Include(s => s.EstadoSolicitud)
                .Where(s => s.IdEstadoSolicitud == 3)
                .ToListAsync();

            return Ok(solicitudesPendientes);
        }

        [HttpPut("aprobar/{id}")]
        public async Task<IActionResult> AprobarSolicitud(int id, [FromBody] RechazoSolicitudDTO aprobacionDto)
        {
            try
            {
                var solicitud = await _context.SolicitudesReservacion.FindAsync(id);
                if (solicitud == null) return NotFound();

                solicitud.IdEstadoSolicitud = 1; // Aprobado
                //solicitud.JustificacionRechazo = aprobacionDto.MotivoAprobacion;

                _context.SolicitudesReservacion.Update(solicitud);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Registra el error para depuración
                Console.WriteLine($"Error al aprobar solicitud: {ex.Message}");
                return StatusCode(500, "Error interno al procesar la solicitud.");
            }
        }

        [HttpPut("rechazar/{id}")]
        public async Task<IActionResult> RechazarSolicitud(int id, [FromBody] RechazoSolicitudDTO rechazoDto)
        {
            var solicitud = await _context.SolicitudesReservacion.FindAsync(id);
            if (solicitud == null) return NotFound();

            if (string.IsNullOrWhiteSpace(rechazoDto.MotivoRechazo))
                return BadRequest("El motivo de rechazo es obligatorio.");

            // Asigna el motivo de rechazo a la propiedad JustificacionRechazo
            solicitud.IdEstadoSolicitud = 2; // Estado Rechazado
            solicitud.JustificacionRechazo = rechazoDto.MotivoRechazo;

            _context.SolicitudesReservacion.Update(solicitud);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Método en el controlador del servidor para devolver el reporte
        [HttpGet("todas")]
        public async Task<ActionResult<IEnumerable<SolicitudReservacionReporteDTO>>> GetReporteSolicitudes()
        {
            var solicitudes = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador)
                .ThenInclude(c => c.Departamento)
                .Include(s => s.EstadoSolicitud)
                .Select(s => new SolicitudReservacionReporteDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    Motivo = s.Motivo,
                    Fecha = s.Fecha,
                    IdEstadoSolicitud = s.IdEstadoSolicitud ?? 0,
                    NombreEstadoSolicitud = s.EstadoSolicitud != null ? s.EstadoSolicitud.NombreEstadoSolicitud : "Desconocido",
                    ColaboradorNombreCompleto = s.Colaborador != null
                        ? $"{s.Colaborador.PrimerNombre} {s.Colaborador.PrimerApellido}"
                        : "No asignado",
                    JustificacionRechazo = s.JustificacionRechazo ?? "No aplica"
                })
                .ToListAsync();

            return Ok(solicitudes);
        }

        // POST api/<SolicitudesReservacionController>
        [HttpPost]
        public async Task<ActionResult<SolicitudReservacion>> PostSolicitudReservacion(SolicitudReservacionDTO solicitudDto)
        {
            // Mapea el DTO al modelo principal
            var solicitudReservacion = new SolicitudReservacion
            {
                Motivo = solicitudDto.Motivo,
                Fecha = solicitudDto.Fecha,
                IdColaborador = solicitudDto.IdColaborador,
                IdEstadoSolicitud = 3
            };

            _context.SolicitudesReservacion.Add(solicitudReservacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSolicitudReservacion), new { id = solicitudReservacion.IdSolicitud }, solicitudReservacion);
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

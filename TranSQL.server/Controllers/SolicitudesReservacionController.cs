using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;
using TranSQL.shared.DTO;
using TranSQL.server.Services;
using Azure.Core;

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

        [HttpGet("{idSolicitud}")]
        public async Task<ActionResult<SolicitudReservacionNotificacionDTO>> GetSolicitudDetalles(int idSolicitud)
        {
            var solicitud = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador)
                .FirstOrDefaultAsync(s => s.IdSolicitud == idSolicitud);

            if (solicitud == null)
            {
                return NotFound();
            }

            var solicitudDto = new SolicitudReservacionNotificacionDTO
            {
                IdSolicitud = solicitud.IdSolicitud,
                Motivo = solicitud.Motivo,
                Fecha = solicitud.Fecha,
                IdColaborador = (int)solicitud.IdColaborador,
                NombreColaborador = $"{solicitud.Colaborador.PrimerNombre} {solicitud.Colaborador.PrimerApellido}"
            };

            return Ok(solicitudDto);
        }



        [HttpPut("aprobar/{id}")]
        public async Task<IActionResult> AprobarSolicitud(int id)
        {
            try
            {
                var solicitud = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador) // Incluye Colaborador
                .FirstOrDefaultAsync(s => s.IdSolicitud == id);

                if (solicitud == null) return NotFound();

                solicitud.IdEstadoSolicitud = 1; // Aprobado
                //solicitud.JustificacionRechazo = aprobacionDto.MotivoAprobacion;

                _context.SolicitudesReservacion.Update(solicitud);
                await _context.SaveChangesAsync();

                await NotificarAprobado(solicitud);
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
            var solicitud = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador) // Incluye Colaborador
                .FirstOrDefaultAsync(s => s.IdSolicitud == id);

            if (solicitud == null) return NotFound();

            if (string.IsNullOrWhiteSpace(rechazoDto.MotivoRechazo))
                return BadRequest("El motivo de rechazo es obligatorio.");

            // Asigna el motivo de rechazo a la propiedad JustificacionRechazo
            solicitud.IdEstadoSolicitud = 2; // Estado Rechazado
            solicitud.JustificacionRechazo = rechazoDto.MotivoRechazo;
            _context.SolicitudesReservacion.Update(solicitud);
            await _context.SaveChangesAsync();

            await NotificarRechazo(solicitud, rechazoDto.MotivoRechazo);
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

        private async Task NotificarRechazo(SolicitudReservacion solicitud, string motivoRechazo)
        {
            string subject = "Solicitud de reservación rechazada";
            string body = $@"
                <html>
                    <body>
                        <h2>Solicitud de Reservación Rechazada</h2>
                        <p>Estimado/a {solicitud.Colaborador.PrimerNombre} {solicitud.Colaborador.PrimerApellido},</p>
                        <p>Lamentamos informarle que su solicitud de reservación ha sido rechazada.</p>
                        <p><strong>Motivo del rechazo:</strong> {motivoRechazo}</p>
                    </body>
                </html>";

            await _emailService.SendEmailAsync(new List<string> { solicitud.Colaborador.Correo }, subject, body, true);
        }

        private async Task NotificarAprobado(SolicitudReservacion solicitud)
        {
            string subject = "Solicitud de reservación Aprobada";
            string body = $@"
                <html>
                    <body>
                        <h2>Solicitud de Reservación Aprobada</h2>
                        <p>Estimado/a {solicitud.Colaborador.PrimerNombre} {solicitud.Colaborador.PrimerApellido},</p>
                        <p>Nos complace informarle que su solicitud de reservación ha sido aprobada.</p>
                        <p>En un momento recibirá otra notificación informando qué vehículo/s le serán asignados.</p>
                    </body>
                </html>";

            await _emailService.SendEmailAsync(new List<string> { solicitud.Colaborador.Correo }, subject, body, true);
        }

        // POST api/<SolicitudesReservacionController>
        [HttpPost]
        public async Task<ActionResult<SolicitudReservacion>> PostSolicitudReservacion(SolicitudReservacionDTO solicitudDto)
        {
            try
            {
                // Busca el colaborador en la base de datos
                var colaborador = await _context.Colaboradores.FindAsync(solicitudDto.IdColaborador);

                if (colaborador == null)
                {
                    return BadRequest("Colaborador no encontrado.");
                }

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

                // Enviar notificación por correo a todos los colaboradores de Logística
                var logisticsEmails = await _context.Colaboradores
                    .Where(c => c.Departamento.NombreDepartamento == "Logística")
                    .Select(c => c.Correo)
                    .ToListAsync();

                string nombreColaborador = $"{colaborador.PrimerNombre} {colaborador.SegundoNombre} {colaborador.PrimerApellido} {colaborador.SegundoApellido} {colaborador.ApellidoDeCasada}";
                string motivoViaje = solicitudDto.Motivo;
                if (logisticsEmails.Any())
                {
                    var subject = "Nueva solicitud de reservación de vehículo";
                    //var body = $"El colaborador {nombreColaborador} ha generado una nueva solicitud de reserva.";

                    string body = $@"
                        <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        margin: 0;
                                        padding: 0;
                                        background-color: #f4f4f4;
                                        color: #333;
                                        display: flex;
                                        justify-content: center;
                                        align-items: center;
                                        height: 100vh;
                                        padding: 20px;
                                    }}
                                    .container {{
                                        max-width: 600px;
                                        background-color: #fff;
                                        border-radius: 8px;
                                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
                                        padding: 20px;
                                        text-align: center;
                                    }}
                                    h2 {{
                                        color: #4CAF50;
                                        font-size: 28px;
                                        margin-bottom: 10px;
                                    }}
                                    p {{
                                        font-size: 16px;
                                        line-height: 1.6;
                                    }}
                                    .button {{
                                        display: inline-block;
                                        padding: 12px 24px;
                                        margin-top: 20px;
                                        color: white;
                                        background-color: #4CAF50;
                                        text-decoration: none;
                                        border-radius: 5px;
                                        font-size: 18px;
                                        transition: background-color 0.3s ease;
                                    }}
                                    .button:hover {{
                                        background-color: #45a049;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <h2>Solicitud de Reservación Creada</h2>
                                    <p>El colaborador <strong>{nombreColaborador}</strong> ha realizado una nueva solicitud de reservación.</p>
                                    <p><strong>Motivo del viaje:</strong> {motivoViaje}</p>
                                    <p>Haz clic en el botón de abajo para ver las solicitudes pendientes:</p>
                                    <a href='https://youtu.be/OZy2jzXuDd4?si=RczQsCcJSONlG4vu' class='button'>
                                        Ver solicitudes pendientes
                                    </a>
                                </div>
                            </body>
                        </html>";


                    await _emailService.SendEmailAsync(logisticsEmails,subject, body, true);
                }

                return CreatedAtAction(nameof(GetSolicitudReservacion), new { id = solicitudReservacion.IdSolicitud }, solicitudReservacion);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la solicitud: {ex.Message}");
                return StatusCode(500, "Error interno al procesar la solicitud.");
            }
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

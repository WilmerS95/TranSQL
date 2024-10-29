using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;
using TranSQL.server.Services;
using System.Text;

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly TranSQLDbContext _context;
        private readonly IEmailService _emailService;

        public AsignacionesController(TranSQLDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // POST: api/Asignacion
        [HttpPost]
        public async Task<IActionResult> AsignarVehiculos(int idSolicitud, List<string> placas)
        {
            // Validar si la solicitud existe
            var solicitud = await _context.SolicitudesReservacion
                .Include(s => s.Colaborador) // Incluye Colaborador para obtener su email
                .FirstOrDefaultAsync(s => s.IdSolicitud == idSolicitud);

            if (solicitud == null)
                return NotFound("La solicitud no existe.");

            // Lista para almacenar los vehículos asignados y generar el mensaje
            var vehiculosAsignados = new List<Vehiculo>();

            foreach (var placa in placas)
            {
                var vehiculo = await _context.Vehiculos
                    .Include(v => v.TipoVehiculo) // Incluye TipoVehiculo para obtener su nombre
                    .FirstOrDefaultAsync(v => v.Placa == placa);

                if (vehiculo == null || vehiculo.IdEstadoVehiculo != 1) // 1 = Disponible
                    return BadRequest($"El vehículo {placa} no está disponible.");

                // Cambiar estado del vehículo
                vehiculo.IdEstadoVehiculo = 2; // 2 = Reservado
                vehiculosAsignados.Add(vehiculo);

                // Crear la asignación
                var asignacion = new Asignacion
                {
                    Placa = placa,
                    IdSolicitud = idSolicitud,
                    IdColaborador = solicitud.IdColaborador ?? 0,
                    IdEstadoSolicitud = solicitud.IdEstadoSolicitud ?? 0
                };

                _context.Asignaciones.Add(asignacion);
                _context.Vehiculos.Update(vehiculo);
            }

            await _context.SaveChangesAsync();

            // Enviar correo de notificación
            await NotificarAprobacion(solicitud, vehiculosAsignados);
            return Ok("Vehículos asignados correctamente y notificación enviada.");
        }

        // Método para enviar el correo de notificación de aprobación y asignación de vehículos
        // Método para enviar el correo de notificación de aprobación y asignación de vehículos
        private async Task NotificarAprobacion(SolicitudReservacion solicitud, List<Vehiculo> vehiculosAsignados)
        {
            // Verificar si la dirección de correo está disponible
            if (string.IsNullOrEmpty(solicitud.Colaborador?.Correo))
            {
                Console.WriteLine("Error: La dirección de correo del colaborador es nula o vacía.");
                return;
            }

            string subject = "Solicitud de reservación aprobada y vehículos asignados";
            string body = $@"
                <html>
                    <body>
                        <h2>Solicitud de Reservación Aprobada</h2>
                        <p>Estimado/a {solicitud.Colaborador.PrimerNombre} {solicitud.Colaborador.PrimerApellido},</p>
                        <p>Nos complace informarle que su solicitud de reservación ha sido aprobada. Se le han asignado los siguientes vehículos:</p>
                        <ul>
                            {string.Join("", vehiculosAsignados.Select(v => $"<li><strong>Placa:</strong> {v.Placa}, <strong>Tipo:</strong> {v.TipoVehiculo?.NombreTipoVehiculo}</li>"))}
                        </ul>
                        <p>Por favor, comuníquese con el departamento de logística para cualquier información adicional.</p>
                        <p>Saludos,</p>
                        <p>El equipo de logística</p>
                    </body>
                </html>";

            Console.WriteLine(subject, body);

            try
            {
                // Intentar enviar el correo
                await _emailService.SendEmailAsync(new List<string> { solicitud.Colaborador.Correo }, subject, body, true);
                Console.WriteLine("Correo de aprobación enviado correctamente.");
            }
            catch (Exception ex)
            {
                // Capturar el error y mostrar en consola
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

    }
}

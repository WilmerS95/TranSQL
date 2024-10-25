using System.Net.Mail;
using System.Net;
using TranSQL.shared.models;
using TranSQL.shared.Services;

namespace TranSQL.client.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarNotificacionLogistica(SolicitudReservacion solicitud)
        {
            var logisticaEmails = await ObtenerCorreosLogistica(); // Obtener correos de logística desde la DB
            var subject = "Nueva solicitud de reservación";
            var body = $"Se ha recibido una nueva solicitud con motivo: {solicitud.Motivo}";

            foreach (var email in logisticaEmails)
            {
                using var smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]))
                {
                    Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage(_config["Smtp:From"], email, subject, body);
                await smtp.SendMailAsync(mailMessage);
            }
        }

        private async Task<List<string>> ObtenerCorreosLogistica()
        {
            // Aquí consultas los correos del departamento de logística desde la base de datos
            // return correos;
            return await Task.FromResult(new List<string> { "logistica@tuempresa.com" });
        }
    }
}

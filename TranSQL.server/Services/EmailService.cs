using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using TranSQL.server.Models;
using TranSQL.shared.models;

namespace TranSQL.server.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(List<string> toEmails, string subject, string body, bool isHtml = true)
        {
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Username),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            // Agregar cada destinatario
            foreach (var email in toEmails)
            {
                mailMessage.To.Add(email);
            }

            await client.SendMailAsync(mailMessage);
        }

        //public async Task SendEmailAsync(string subject, string body, List<string> recipients)
        //{
        //    using var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        //    {
        //        Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
        //        EnableSsl = _smtpSettings.EnableSsl
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_smtpSettings.Username),
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    };

        //    foreach (var recipient in recipients)
        //    {
        //        mailMessage.To.Add(recipient);
        //    }

        //    await smtpClient.SendMailAsync(mailMessage);
        //}

        // 1. Notificar al departamento de logística sobre nueva solicitud
        //public async Task NotifyLogisticsOnNewRequest(Colaborador requester)
        //{
        //    var logisticsEmails = GetDepartmentEmails("Logística"); // Método para obtener correos de Logística
        //    var subject = "Nueva solicitud de reservación de vehículo";
        //    //var body = $"El colaborador {requester.PrimerNombre} {requester.PrimerApellido} ha generado una nueva solicitud de reserva.";

        //    string body = $@"
        //<html>
        //    <body>
        //        <h2>Solicitud de Reservación Creada</h2>
        //        <p>El colaborador <strong>{requester.PrimerNombre} {requester.PrimerApellido}</strong> ha realizado una nueva solicitud de reservación.</p>
        //        <p>Haz clic en el botón de abajo para ver las solicitudes pendientes:</p>
        //        <a href='https://youtu.be/OZy2jzXuDd4?si=RczQsCcJSONlG4vu' 
        //           style='display: inline-block; padding: 10px 20px; color: white; background-color: #4CAF50; text-align: center; text-decoration: none; border-radius: 5px;'>
        //            Ver Solicitudes Pendientes
        //        </a>
        //    </body>
        //</html>";

        //    await SendEmailAsync(logisticsEmails, subject, body, true);
        //}

        // 2. Notificar al solicitante sobre el rechazo de su solicitud
        //public async Task NotifyRequesterOnRejection(Colaborador requester)
        //{
        //    var subject = "Su solicitud de reservación ha sido rechazada";
        //    var body = $"Estimado {requester.PrimerNombre}, su solicitud de reservación de vehículo ha sido rechazada.";

        //    await SendEmailAsync(subject, body, new List<string> { requester.Correo });
        //}

        // 3. Notificar al solicitante y a seguridad cuando la solicitud es aprobada
        //public async Task NotifyOnApproval(Colaborador requester, List<string> securityEmails)
        //{
        //    var subject = "Solicitud de reservación aprobada";
        //    var body = $"Estimado {requester.PrimerNombre}, su solicitud ha sido aprobada.";

        //    var recipients = new List<string> { requester.Correo };
        //    recipients.AddRange(securityEmails);

        //    await SendEmailAsync(subject, body, recipients);
        //}

        private List<string> GetDepartmentEmails(string departmentName)
        {
            // Simulación de obtención de correos desde la base de datos
            // Reemplaza esta función para consultar la base de datos real
            return departmentName == "Logística"
                ? new List<string> { "wilmerantoniosinay@gmail.com", "krodasa7@miumg.edu.gt" }
                : new List<string> { "wilmerantoniosinay@gmail.com" }; //seguridad1@empresa.com
        }
    }
}

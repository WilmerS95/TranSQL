using TranSQL.shared.models;

namespace TranSQL.shared.Services
{
    public interface IEmailService
    {
        Task EnviarNotificacionLogistica(SolicitudReservacion solicitud);
    }
}

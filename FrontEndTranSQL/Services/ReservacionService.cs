using System.Net.Http.Json;
using TranSQL.shared.DTO;

namespace TranSQL.client.Services
{
    public class ReservacionService
    {
        private readonly HttpClient _httpClient;

        public ReservacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todas las reservaciones
        public async Task<List<ReservacionDTO>> GetReservacionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservacionDTO>>("/api/Reservaciones");
        }

        // Crear nueva reservación
        public async Task<HttpResponseMessage> CrearReservacionAsync(ReservacionDTO nuevaReservacion)
        {
            return await _httpClient.PostAsJsonAsync("/api/Reservaciones", nuevaReservacion);
        }

        // Eliminar reservación
        public async Task<HttpResponseMessage> EliminarReservacionAsync(int idReservacion)
        {
            return await _httpClient.DeleteAsync($"/api/Reservaciones/{idReservacion}");
        }
    }
}

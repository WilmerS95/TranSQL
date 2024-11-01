using System.Net.Http.Json;
using TranSQL.shared.DTO;

namespace TranSQL.client.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly HttpClient _httpClient;

        public VehiculoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VehiculoAsignacionDTO>> ObtenerVehiculosAsignadosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VehiculoAsignacionDTO>>("api/vehiculos");
        }

        public async Task<bool> UpdateVehiculoEstadoAsync(string placa, int nuevoEstado)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/vehiculos/estado/{placa}", nuevoEstado);
            return response.IsSuccessStatusCode;
        }
    }
}

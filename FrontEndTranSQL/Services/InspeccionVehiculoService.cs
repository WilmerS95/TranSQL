using System.Net.Http.Json;
using TranSQL.shared.models;

namespace TranSQL.client.Services
{
    public class InspeccionVehiculoService
    {
        private readonly HttpClient _httpClient;

        public InspeccionVehiculoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InspeccionVehiculo>> GetInspeccionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InspeccionVehiculo>>("api/InspeccionVehiculos");
        }

        public async Task<InspeccionVehiculo> GetInspeccionByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InspeccionVehiculo>($"api/InspeccionVehiculos/{id}");
        }

        public async Task<bool> CreateInspeccionAsync(InspeccionVehiculo inspeccion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/InspeccionVehiculos", inspeccion);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInspeccionAsync(int id, InspeccionVehiculo inspeccion)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/InspeccionVehiculos/{id}", inspeccion);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInspeccionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/InspeccionVehiculos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

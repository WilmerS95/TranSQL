using System.Net.Http.Json;
using TranSQL.shared;

namespace TranSQL.client.Services
{
    public class VehiculoService
    {
        private readonly HttpClient _httpClient;

        public VehiculoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Vehiculo>?> GetVehiculosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Vehiculo>>("/api/Vehiculos");
        }
    }
}

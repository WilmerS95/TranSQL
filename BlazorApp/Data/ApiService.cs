using System.Net.Http.Json;
using TranSQL.server;

namespace BlazorApp.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obtener la lista de vehículos
        public async Task<List<Vehiculo>> GetVehiculosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Vehiculo>>("api/Vehiculos");
        }

        // Método para obtener un vehículo por su placa
        public async Task<Vehiculo> GetVehiculoAsync(string placa)
        {
            return await _httpClient.GetFromJsonAsync<Vehiculo>($"api/Vehiculos/{placa}");
        }

        // Método para crear un nuevo vehículo
        public async Task<Vehiculo> CreateVehiculoAsync(Vehiculo vehiculo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Vehiculos", vehiculo);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Vehiculo>();
        }

        // Método para actualizar un vehículo existente
        public async Task UpdateVehiculoAsync(string placa, Vehiculo vehiculo)
        {
            await _httpClient.PutAsJsonAsync($"api/Vehiculos/{placa}", vehiculo);
        }

        // Método para eliminar un vehículo
        public async Task DeleteVehiculoAsync(string placa)
        {
            await _httpClient.DeleteAsync($"api/Vehiculos/{placa}");
        }

        // Método para obtener la lista de tipos de vehículos
        public async Task<List<TipoVehiculo>> GetTipoVehiculosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoVehiculo>>("api/TipoVehiculos");
        }

        // Método para crear un nuevo tipo de vehículo
        public async Task<TipoVehiculo> CreateTipoVehiculoAsync(TipoVehiculo tipoVehiculo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TipoVehiculos", tipoVehiculo);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TipoVehiculo>();
        }
    }
}

using System.Net.Http.Headers;
using System.Net.Http.Json;
using TranSQL.shared.models;
using Microsoft.AspNetCore.Components.Forms;
using TranSQL.shared.DTO;
using System.Net.Http;

namespace TranSQL.client.Services
{
    public class InspeccionVehiculoService
    {
        private readonly HttpClient _httpClient;

        public InspeccionVehiculoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InspeccionVehiculoDTO>> GetInspeccionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InspeccionVehiculoDTO>>("api/InspeccionVehiculos");
        }

        public async Task<List<Vehiculo>> GetVehiculosPorEstado(string estado)
        {
            // Realiza una llamada a la API para obtener los vehículos por estado
            var response = await _httpClient.GetFromJsonAsync<List<Vehiculo>>($"api/vehiculos/estado/{estado}");
            return response ?? new List<Vehiculo>();
        }


        public async Task CreateInspeccionAsync(InspeccionVehiculo inspeccion)
        {
            await _httpClient.PostAsJsonAsync("api/InspeccionVehiculos", inspeccion);
        }

        public async Task UpdateInspeccionAsync(int id, InspeccionVehiculo inspeccion)
        {
            await _httpClient.PutAsJsonAsync($"api/InspeccionVehiculos/{id}", inspeccion);
        }

        public async Task DeleteInspeccionAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/InspeccionVehiculos/{id}");
        }

        public async Task<bool> SubirImagenAsync(int inspeccionId, IBrowserFile imagen)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(imagen.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(imagen.ContentType);
            content.Add(fileContent, "imagen", imagen.Name);

            var response = await _httpClient.PostAsync($"api/InspeccionVehiculos/{inspeccionId}/imagen", content);
            return response.IsSuccessStatusCode;
        }
    }
}

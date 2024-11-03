using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TranSQL.shared.DTO;
using Microsoft.AspNetCore.Components.Forms;
using TranSQL.shared.models;
namespace TranSQL.client.Services
{
    public class InspeccionService
    {
        private readonly HttpClient _httpClient;

        public InspeccionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SubirImagenAsync(int idInspeccion, IBrowserFile imagen)
        {
            var content = new MultipartFormDataContent();
            var stream = imagen.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // Max 10MB
            content.Add(new StreamContent(stream), "imagen", imagen.Name);

            var response = await _httpClient.PostAsync($"api/inspeccionvehiculos/{idInspeccion}/imagen", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<InspeccionVehiculo> ObtenerInspeccionAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InspeccionVehiculo>($"api/inspeccionvehiculos/{id}");
        }
    }
}

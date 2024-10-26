using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TranSQL.shared;

public class VehiculoService
{
    private readonly HttpClient _httpClient;

    public VehiculoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Vehiculo>> GetVehiculosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Vehiculo>>("api/vehiculos");
    }

    public async Task<Vehiculo> GetVehiculoAsync(string placa)
    {
        return await _httpClient.GetFromJsonAsync<Vehiculo>($"api/vehiculos/{placa}");
    }

    public async Task AddVehiculoAsync(Vehiculo vehiculo)
    {
        var response = await _httpClient.PostAsJsonAsync("api/vehiculos", vehiculo);
        response.EnsureSuccessStatusCode();
    }
}

using TranSQL.shared.DTO;

namespace TranSQL.client.Services
{
    public interface IVehiculoService
    {
        Task<List<VehiculoAsignacionDTO>> ObtenerVehiculosAsignadosAsync();
        Task<bool> UpdateVehiculoEstadoAsync(string placa, int nuevoEstado);
    }
}

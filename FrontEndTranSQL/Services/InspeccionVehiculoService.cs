using System.Collections.Generic;
using System.Threading.Tasks;
using TranSQL.shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TranSQL.client.Services
{
    public class InspeccionVehiculoService
    {
        // Datos simulados
        private static List<InspeccionVehiculoDTO> inspeccionesSimuladas = new List<InspeccionVehiculoDTO>
        {
            new InspeccionVehiculoDTO { IdInspeccion = 1, FechaInspeccion = DateTime.Now, Observaciones = "Primera inspección", OdometroInicial = 5000, OdometroFinal = 5500 },
            new InspeccionVehiculoDTO { IdInspeccion = 2, FechaInspeccion = DateTime.Now.AddDays(-1), Observaciones = "Segunda inspección", OdometroInicial = 2000, OdometroFinal = 2500 }
        };

        private static List<VehiculoInspeccionDTO> vehiculosSimulados = new List<VehiculoInspeccionDTO>
        {
            new VehiculoInspeccionDTO { Placa = "ABC123", Modelo = 2020, EstadoVehiculo = "Solicitado", OdometroInicial = 15000, OdometroFinal = 20000 },
            new VehiculoInspeccionDTO { Placa = "DEF456", Modelo = 2021, EstadoVehiculo = "En Ruta", OdometroInicial = 3000, OdometroFinal = 4000 }
        };

        public Task<List<InspeccionVehiculoDTO>> GetInspeccionesAsync()
        {
            return Task.FromResult(inspeccionesSimuladas);
        }

        public Task<List<VehiculoInspeccionDTO>> GetVehiculosPorEstado(string estado)
        {
            var vehiculosPorEstado = vehiculosSimulados.FindAll(v => v.EstadoVehiculo == estado);
            return Task.FromResult(vehiculosPorEstado);
        }

        public Task CreateInspeccionAsync(InspeccionVehiculoDTO inspeccion)
        {
            inspeccion.IdInspeccion = inspeccionesSimuladas.Count + 1;
            inspeccionesSimuladas.Add(inspeccion);
            return Task.CompletedTask;
        }

        public Task UpdateInspeccionAsync(int id, InspeccionVehiculoDTO inspeccion)
        {
            var existingInspeccion = inspeccionesSimuladas.Find(i => i.IdInspeccion == id);
            if (existingInspeccion != null)
            {
                existingInspeccion.FechaInspeccion = inspeccion.FechaInspeccion;
                existingInspeccion.Observaciones = inspeccion.Observaciones;
                existingInspeccion.OdometroInicial = inspeccion.OdometroInicial;
                existingInspeccion.OdometroFinal = inspeccion.OdometroFinal;
            }
            return Task.CompletedTask;
        }

        public Task<bool> SubirImagenAsync(int inspeccionId, IBrowserFile imagen)
        {
            // Simulación de subida de imagen, siempre retorna true
            return Task.FromResult(true);
        }

        public Task CambiarEstadoVehiculo(string placa, string nuevoEstado)
        {
            var vehiculo = vehiculosSimulados.Find(v => v.Placa == placa);
            if (vehiculo != null)
            {
                vehiculo.EstadoVehiculo = nuevoEstado;
            }
            return Task.CompletedTask;
        }
    }
}

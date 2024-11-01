using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class VehiculoAsignacionDTO
    {
        public string Placa { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdEstadoVehiculo { get; set; }

        // Cambia a string para que coincida con la respuesta JSON
        public string TipoVehiculo { get; set; } = string.Empty;
        public string EstadoVehiculo { get; set; } = string.Empty;

        public List<AsignacionDTO> Asignaciones { get; set; } = new List<AsignacionDTO>();
    }

}

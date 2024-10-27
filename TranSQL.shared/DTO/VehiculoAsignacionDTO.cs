using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class VehiculoAsignacionDTO
    {
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdEstadoVehiculo { get; set; }
        public TipoVehiculoDTO TipoVehiculo { get; set; }
        public EstadoVehiculoDTO EstadoVehiculo { get; set; }
        public List<AsignacionDTO> Asignaciones { get; set; }
    }
}

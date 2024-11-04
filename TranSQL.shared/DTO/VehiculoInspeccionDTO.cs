using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class VehiculoInspeccionDTO
    {
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public string EstadoVehiculo { get; set; }
        public int? OdometroInicial { get; set; }
        public int? OdometroFinal { get; set; }
        public AsignacionInfoDTO Asignacion { get; set; }
    }
}

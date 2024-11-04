using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class VehiculoManagementDTO
    {
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public string EstadoVehiculo { get; set; }
        public AsignacionInfoDTO Asignacion { get; set; }
    }
}

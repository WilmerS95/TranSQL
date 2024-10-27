using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class EstadoVehiculoDTO
    {
        public int IdEstadoVehiculo { get; set; }
        public string NombreEstadoVehiculo { get; set; }
        public List<string> Vehiculos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class TipoVehiculoDTO
    {
        public int IdTipoVehiculo { get; set; }
        public string NombreTipoVehiculo { get; set; }
        public List<string> Vehiculos { get; set; }
    }
}

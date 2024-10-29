using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class VehiculoSeleccionado
    {
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public bool Seleccionado { get; set; }
        public int IdTipoVehiculo { get; set; }
    }
}

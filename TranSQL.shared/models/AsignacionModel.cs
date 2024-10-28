using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class AsignacionModel
    {
        public int IdSolicitud { get; set; }
        public List<VehiculoSeleccionado> VehiculosSeleccionados { get; set; }
    }
}

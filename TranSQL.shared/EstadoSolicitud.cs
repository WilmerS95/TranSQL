using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class EstadoSolicitud
    {
        private int IdEstadoSolicitud {  get; set; }

        [Required]
        [StringLength(50)]
        private string NombreEstadoSolicitud { get; set;} = string.Empty;

        public EstadoSolicitud(string nombreEstadoSolicitud)
        {
            NombreEstadoSolicitud = nombreEstadoSolicitud ?? throw new ArgumentNullException(nameof(nombreEstadoSolicitud));
        }

        public EstadoSolicitud()
        {
        }
    }
}

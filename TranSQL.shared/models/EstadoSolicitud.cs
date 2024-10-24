using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class EstadoSolicitud
    {
        [Key]
        public int IdEstadoSolicitud { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreEstadoSolicitud { get; set; } = string.Empty;

        public EstadoSolicitud(string nombreEstadoSolicitud)
        {
            NombreEstadoSolicitud = nombreEstadoSolicitud ?? throw new ArgumentNullException(nameof(nombreEstadoSolicitud));
        }

        public EstadoSolicitud()
        {
        }
    }
}

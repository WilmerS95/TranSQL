using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class SolicitudReservacionReporteDTO
    {
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string NombreEstadoSolicitud { get; set; }
        public string ColaboradorNombreCompleto { get; set; }
        public string JustificacionRechazo { get; set; }
    }
}

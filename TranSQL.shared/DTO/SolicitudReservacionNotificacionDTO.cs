using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class SolicitudReservacionNotificacionDTO
    {
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdColaborador { get; set; }
        public string NombreColaborador { get; set; }
    }
}

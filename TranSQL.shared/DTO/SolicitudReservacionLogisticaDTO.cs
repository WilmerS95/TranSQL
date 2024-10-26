using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class SolicitudReservacionLogisticaDTO
    {
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public EstadoSolicitudDTO EstadoSolicitud { get; set; }
        public ColaboradorDTO Colaborador { get; set; }
        public string MotivoAccion { get; set; }
        public bool IsRechazarIntentado { get; set; } = false;
    }
}

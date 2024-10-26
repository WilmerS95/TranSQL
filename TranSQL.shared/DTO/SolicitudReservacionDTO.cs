using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class SolicitudReservacionDTO
    {
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int? IdColaborador { get; set; }
        public ColaboradorDTO Colaborador { get; set; }
        public EstadoSolicitudDTO EstadoSolicitud { get; set; }
    }
}

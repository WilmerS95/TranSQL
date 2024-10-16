using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class SolicitudReservacion
    {
        private int IdSolicitud {  get; set; }

        [Required]
        [StringLength(300)]
        private string Motivo { get; set; } = string.Empty;

        [Required]
        private DateTime Fecha { get; set; }

        private int IdColaborador { get; set; }
        private int IdEstadoSolicitud { get; set; }
        private string JustificacionRechazo { get; set; } = string.Empty;

        private Colaborador colaborador;
        private EstadoSolicitud estadoSolicitud;

        public SolicitudReservacion(int idSolicitud, string motivo, DateTime fecha, int idColaborador, int idEstadoSolicitud, string justificacionRechazo, Colaborador colaborador, EstadoSolicitud estadoSolicitud)
        {
            IdSolicitud = idSolicitud;
            Motivo = motivo ?? throw new ArgumentNullException(nameof(motivo));
            this.Fecha = fecha;
            this.IdColaborador = idColaborador;
            this.IdEstadoSolicitud = idEstadoSolicitud;
            this.JustificacionRechazo = justificacionRechazo ?? throw new ArgumentNullException(nameof(justificacionRechazo));
            this.colaborador = colaborador ?? throw new ArgumentNullException(nameof(colaborador));
            this.estadoSolicitud = estadoSolicitud ?? throw new ArgumentNullException(nameof(estadoSolicitud));
        }

        public SolicitudReservacion()
        {
        }
    }
}

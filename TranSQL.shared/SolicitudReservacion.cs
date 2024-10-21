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
        [Key]
        public int IdSolicitud {  get; set; }

        [Required]
        [StringLength(300)]
        public string Motivo { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        public int IdColaborador { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string JustificacionRechazo { get; set; } = string.Empty;

        public Colaborador colaborador;
        public EstadoSolicitud estadoSolicitud;

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranSQL.shared.models
{
    public class SolicitudReservacion
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required]
        [StringLength(300)]
        public string Motivo { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        public int? IdColaborador { get; set; }
        public int? IdEstadoSolicitud { get; set; }
        public string? JustificacionRechazo { get; set; }

        [ForeignKey("IdColaborador")]
        public Colaborador Colaborador { get; set; }

        [ForeignKey("IdEstadoSolicitud")]
        public EstadoSolicitud EstadoSolicitud { get; set; }

        // Relación con Asignacion
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();

        public SolicitudReservacion() { }

        public SolicitudReservacion(int idSolicitud, string motivo, DateTime fecha, int idColaborador, int idEstadoSolicitud, string justificacionRechazo, Colaborador colaborador, EstadoSolicitud estadoSolicitud)
        {
            IdSolicitud = idSolicitud;
            Motivo = motivo ?? throw new ArgumentNullException(nameof(motivo));
            Fecha = fecha;
            IdColaborador = idColaborador;
            IdEstadoSolicitud = idEstadoSolicitud;
            JustificacionRechazo = justificacionRechazo ?? throw new ArgumentNullException(nameof(justificacionRechazo));
            Colaborador = colaborador ?? throw new ArgumentNullException(nameof(colaborador));
            EstadoSolicitud = estadoSolicitud ?? throw new ArgumentNullException(nameof(estadoSolicitud));
        }
    }

}

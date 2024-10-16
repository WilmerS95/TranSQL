using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TranSQL.shared
{
    public class Reservacion
    {
        private int IdReservacion {  get; set; }

        [Required]
        private DateTime FechaReservacion { get; set; }

        [Required]
        private DateTime FechaInicio {  get; set; }

        [Required]
        private DateTime FechaFin {  get; set; }

        [Required]
        [StringLength(300)]
        private string Observaciones {  get; set; } = string.Empty;
        private int IdSolicitud { get; set; }

        private string Placa { get; set; } = string.Empty;

        private SolicitudReservacion solicitudReservacion;
        private Vehiculo vehiculo;

        public Reservacion(int idReservacion, DateTime fechaReservacion, DateTime fechaInicio, DateTime fechaFin, string observaciones, int idSolicitud, string placa, SolicitudReservacion solicitudReservacion, Vehiculo vehiculo)
        {
            IdReservacion = idReservacion;
            FechaReservacion = fechaReservacion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Observaciones = observaciones ?? throw new ArgumentNullException(nameof(observaciones));
            IdSolicitud = idSolicitud;
            Placa = placa ?? throw new ArgumentNullException(nameof(placa));
            this.solicitudReservacion = solicitudReservacion ?? throw new ArgumentNullException(nameof(solicitudReservacion));
            this.vehiculo = vehiculo ?? throw new ArgumentNullException(nameof(vehiculo));
        }

        public Reservacion()
        {
        }
    }
}

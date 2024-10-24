using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TranSQL.shared.models
{
    public class Reservacion
    {
        [Key]
        public int IdReservacion { get; set; }

        [Required]
        public DateTime FechaReservacion { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [StringLength(300)]
        public string Observaciones { get; set; } = string.Empty;
        public int IdSolicitud { get; set; }

        public string Placa { get; set; } = string.Empty;

        public SolicitudReservacion solicitudReservacion;
        public Vehiculo vehiculo;

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

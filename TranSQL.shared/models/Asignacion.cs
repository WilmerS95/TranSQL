using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class Asignacion
    {
        [Key]
        public int IdAsignacion { get; set; }

        // Llave foránea para Vehiculo
        [Required]
        [StringLength(10)]
        public string Placa { get; set; }
        public Vehiculo Vehiculo { get; set; }

        // Llave foránea para SolicitudReservacion
        [Required]
        public int IdSolicitud { get; set; }
        public SolicitudReservacion SolicitudReservacion { get; set; }

        public int IdColaborador { get; set; }
        public int IdEstadoSolicitud { get; set; }

        public Colaborador Colaborador { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }

        public Asignacion() { }

        public Asignacion(int idAsignacion, string placa, int idSolicitud, int idColaborador, int idEstadoSolicitud)
        {
            IdAsignacion = idAsignacion;
            Placa = placa ?? throw new ArgumentNullException(nameof(placa));
            IdSolicitud = idSolicitud;
            IdColaborador = idColaborador;
            IdEstadoSolicitud = idEstadoSolicitud;
        }
    }

}

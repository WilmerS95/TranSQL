using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Asignacion
    {
        [Key]
        public int idAsignacion { get; set; }
        public int idColaborador { get; set; }
        public int idEstadoSolicitud {  get; set; }
        public Colaborador colaborador;
        public EstadoSolicitud estadoSolicitud;

        public Asignacion(int idAsignacion, int idColaborador, int idEstadoSolicitud, Colaborador colaborador, EstadoSolicitud estadoSolicitud)
        {
            this.idAsignacion = idAsignacion;
            this.idColaborador = idColaborador;
            this.idEstadoSolicitud = idEstadoSolicitud;
            this.colaborador = colaborador ?? throw new ArgumentNullException(nameof(colaborador));
            this.estadoSolicitud = estadoSolicitud ?? throw new ArgumentNullException(nameof(estadoSolicitud));
        }

        public Asignacion()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Asignacion
    {
        private int idAsignacion { get; set; }
        private int idColaborador { get; set; }
        private int idEstadoSolicitud {  get; set; }

        private Colaborador colaborador;
        private EstadoSolicitud estadoSolicitud;

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

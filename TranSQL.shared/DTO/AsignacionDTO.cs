using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class AsignacionDTO
    {
        public int IdAsignacion { get; set; }
        public string Placa { get; set; }
        public VehiculoDTO Vehiculo { get; set; }
        public int IdSolicitud { get; set; }
        public SolicitudReservacionDTO SolicitudReservacion { get; set; }
        public int IdColaborador { get; set; }
        public ColaboradorDTO Colaborador { get; set; }
        public EstadoSolicitudDTO EstadoSolicitud { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class SolicitudReservacionLogisticaVehiculoDTO
    {
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdEstadoSolicitud { get; set; }
        public string NombreEstadoSolicitud { get; set; }
        public string ColaboradorNombreCompleto { get; set; }
        public string JustificacionRechazo { get; set; }

        // Agrega esta propiedad para evitar el error
        public int IdColaborador { get; set; }

        // Cambia el tipo de EstadoSolicitud de string a EstadoSolicitudDTO
        public EstadoSolicitudDTO EstadoSolicitud { get; set; }

        // Relación con Colaborador (opcional si se necesita en el controlador)
        public ColaboradorDTO Colaborador { get; set; }
    }

}

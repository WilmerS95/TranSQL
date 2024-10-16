using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class InspeccionVehiculo
    {
        private int IdInspeccion {  get; set; }

        private DateTime FechaInspeccion {  set; get; }

        [Required]
        [StringLength(300)]
        private string Observaciones { get; set; } = string.Empty;
        private int OdometroInicial { get; set; }
        private int OdometroFinal { get; set; }
        private int IdReservacion {  get; set; }
        private int IdColaborador { get; set; }
        private int IdAccesorio { get; set; }
        private int IdTipoInspeccion { get; set; }

        private Reservacion reservacion;
        private Colaborador colaborador;
        private Accesorio accesorio;
        private TipoInspeccion tipoInspeccion;

        public InspeccionVehiculo(int idInspeccion, DateTime fechaInspeccion, string observaciones, int odometroInicial, int odometroFinal, int idReservacion, int idColaborador, int idAccesorio, int idTipoInspeccion, Reservacion reservacion, Colaborador colaborador, Accesorio accesorio, TipoInspeccion tipoInspeccion)
        {
            IdInspeccion = idInspeccion;
            FechaInspeccion = fechaInspeccion;
            Observaciones = observaciones ?? throw new ArgumentNullException(nameof(observaciones));
            OdometroInicial = odometroInicial;
            OdometroFinal = odometroFinal;
            IdReservacion = idReservacion;
            IdColaborador = idColaborador;
            IdAccesorio = idAccesorio;
            IdTipoInspeccion = idTipoInspeccion;
            this.reservacion = reservacion ?? throw new ArgumentNullException(nameof(reservacion));
            this.colaborador = colaborador ?? throw new ArgumentNullException(nameof(colaborador));
            this.accesorio = accesorio ?? throw new ArgumentNullException(nameof(accesorio));
            this.tipoInspeccion = tipoInspeccion ?? throw new ArgumentNullException(nameof(tipoInspeccion));
        }

        public InspeccionVehiculo()
        {
        }
    }
}

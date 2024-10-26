using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class InspeccionVehiculo
    {
        [Key]
        public int IdInspeccion { get; set; }

        public DateTime FechaInspeccion { set; get; }

        [Required]
        [StringLength(300)]
        public string Observaciones { get; set; } = string.Empty;
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public int IdReservacion { get; set; }
        public int IdColaborador { get; set; }
        public int IdAccesorio { get; set; }
        public int IdTipoInspeccion { get; set; }

        public Reservacion reservacion;
        public Colaborador colaborador;
        public Accesorio accesorio;
        public TipoInspeccion tipoInspeccion;

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class InspeccionAccesorio
    {
        [Key]
        public int IdInspeccion {  get; set; }
        public int IdAccesorio { get; set; }
        [Required]
        [StringLength(75)]
        public string EstadoAccesorio { get; set; } = string.Empty;

        public InspeccionVehiculo inspeccionVehiculo;
        public Accesorio accesorio;

        public InspeccionAccesorio(int idInspeccion, int idAccesorio, string estadoAccesorio, InspeccionVehiculo inspeccionVehiculo, Accesorio accesorio)
        {
            IdInspeccion = idInspeccion;
            IdAccesorio = idAccesorio;
            EstadoAccesorio = estadoAccesorio ?? throw new ArgumentNullException(nameof(estadoAccesorio));
            this.inspeccionVehiculo = inspeccionVehiculo ?? throw new ArgumentNullException(nameof(inspeccionVehiculo));
            this.accesorio = accesorio ?? throw new ArgumentNullException(nameof(accesorio));
        }

        public InspeccionAccesorio()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class EstadoVehiculo
    {
        [Key]
        public int IdEstadoVehiculo { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreEstadoVehiculo { set; get; } = string.Empty;
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
        //public List<Vehiculo>? Vehiculos { get; set; }

        public EstadoVehiculo(string nombreEstadoVehiculo)
        {
            NombreEstadoVehiculo = nombreEstadoVehiculo ?? throw new ArgumentNullException(nameof(nombreEstadoVehiculo));
        }

        public EstadoVehiculo()
        {
        }
    }
}

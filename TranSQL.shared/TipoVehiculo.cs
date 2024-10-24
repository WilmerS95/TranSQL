using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class TipoVehiculo
    {
        [Key]
        public int IdTipoVehiculo {  get; set; }

        [Required]
        [StringLength(100)]
        public string NombreTipoVehiculo { get; set;} = string.Empty;
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
        //public List<Vehiculo>? Vehiculos { get; set; }

        public TipoVehiculo(string nombreTipoVehiculo)
        {
            NombreTipoVehiculo = nombreTipoVehiculo ?? throw new ArgumentNullException(nameof(nombreTipoVehiculo));
        }

        public TipoVehiculo()
        {
        }
    }
}

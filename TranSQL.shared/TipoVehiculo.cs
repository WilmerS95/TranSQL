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
        private int IdTipoVehiculo {  get; set; }

        [Required]
        [StringLength(100)]
        private string NombreTipoVehiculo { get; set;} = string.Empty;

        public TipoVehiculo(string nombreTipoVehiculo)
        {
            NombreTipoVehiculo = nombreTipoVehiculo ?? throw new ArgumentNullException(nameof(nombreTipoVehiculo));
        }

        public TipoVehiculo()
        {
        }
    }
}

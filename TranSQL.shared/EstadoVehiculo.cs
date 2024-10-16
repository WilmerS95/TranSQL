using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class EstadoVehiculo
    {
        private int IdEstadoVehiculo {  get; set; }

        [Required]
        [StringLength(100)]
        private string NombreEstadoVehiculo {  set; get; } = string.Empty;

        public EstadoVehiculo(string nombreEstadoVehiculo)
        {
            NombreEstadoVehiculo = nombreEstadoVehiculo ?? throw new ArgumentNullException(nameof(nombreEstadoVehiculo));
        }

        public EstadoVehiculo()
        {
        }
    }
}

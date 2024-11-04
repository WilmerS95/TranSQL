using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class CreateVehiculoDTO
    {
        [Required]
        public string Placa { get; set; } = string.Empty;

        [Required]
        public int Modelo { get; set; }

        [Required]
        public int OdometroInicial { get; set; }

        public int OdometroFinal { get; set; } = 0;

        [Required]
        public int IdTipoVehiculo { get; set; }

        [Required]
        public int IdEstadoVehiculo { get; set; }
    }
}

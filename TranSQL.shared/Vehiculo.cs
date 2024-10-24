using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Vehiculo
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string Placa {  get; set; } = string.Empty;

        [Required]
        public int Modelo { get; set; }

        public int OdometroInicial { get; set; }

        public int OdometroFinal { get; set; }

        public int IdTipoVehiculo {  get; set; }

        private int IdEstadoVehiculo { get; set; }

        public TipoVehiculo tipoVehiculo;

        public EstadoVehiculo estadoVehiculo;

        public Vehiculo(string placa, int modelo, int odometroInicial, int odometroFinal, int idTipoVehiculo, int idEstadoVehiculo, TipoVehiculo tipoVehiculo, EstadoVehiculo estadoVehiculo)
        {
            Placa = placa ?? throw new ArgumentNullException(nameof(placa));
            Modelo = modelo;
            OdometroInicial = odometroInicial;
            OdometroFinal = odometroFinal;
            IdTipoVehiculo = idTipoVehiculo;
            IdEstadoVehiculo = idEstadoVehiculo;
            this.tipoVehiculo = tipoVehiculo ?? throw new ArgumentNullException(nameof(tipoVehiculo));
            this.estadoVehiculo = estadoVehiculo ?? throw new ArgumentNullException(nameof(estadoVehiculo));
        }

        public Vehiculo()
        {
        }
    }
}

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
        [Required]
        [StringLength(10)]
        private string Placa {  get; set; } = string.Empty;

        [Required]
        private int Modelo { get; set; }

        private int OdometroInicial { get; set; }

        private int OdometroFinal { get; set; }

        private int IdTipoVehiculo {  get; set; }

        private int IdEstadoVehiculo { get; set; }

        private TipoVehiculo tipoVehiculo;

        private EstadoVehiculo estadoVehiculo;

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

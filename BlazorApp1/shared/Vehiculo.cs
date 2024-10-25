namespace TranSQL.shared
{
    public class Vehiculo
    {
        public string Placa { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdEstadoVehiculo { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        public EstadoVehiculo EstadoVehiculo { get; set; }
    }
}

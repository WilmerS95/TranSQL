using System;

namespace TranSQL.shared.DTO
{
    public class InspeccionVehiculoDTO
    {
        public int IdInspeccion { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public string Observaciones { get; set; }
        public int OdometroInicial { get; set; }
        public int OdometroFinal { get; set; }
        public string ImagenRuta { get; set; }
        public int IdReservacion { get; set; }
        public string ColaboradorNombre { get; set; }
        public string AccesorioNombre { get; set; }
        public string TipoInspeccionNombre { get; set; }
    }
}

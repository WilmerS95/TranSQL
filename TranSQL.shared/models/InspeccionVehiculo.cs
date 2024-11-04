using System;
using System.ComponentModel.DataAnnotations;

namespace TranSQL.shared.models
{
    public class InspeccionVehiculo
    {
        [Key]
        public int IdInspeccion { get; set; }

        public DateTime FechaInspeccion { get; set; }

        [StringLength(300)]
        public string? Observaciones { get; set; } = string.Empty;

        public int? OdometroInicial { get; set; }
        public int? OdometroFinal { get; set; }
        public int IdReservacion { get; set; }
        public int? IdColaborador { get; set; }
        public int? IdAccesorio { get; set; }
        public int? IdTipoInspeccion { get; set; }

        [StringLength(255)]
        public string? ImagenRuta { get; set; }
        public Reservacion? Reservacion { get; set; }
        public Colaborador? Colaborador { get; set; }
        public Accesorio? Accesorio { get; set; }
        public TipoInspeccion? TipoInspeccion { get; set; }
    }
}

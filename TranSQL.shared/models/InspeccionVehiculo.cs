using System;
using System.ComponentModel.DataAnnotations;

namespace TranSQL.shared.models
{
    public class InspeccionVehiculo
    {
        [Key]
        public int IdInspeccion { get; set; }
        public DateTime FechaInspeccion { get; set; }

        [Required]
        [StringLength(300)]
        public string Observaciones { get; set; } = string.Empty;

        public int? OdometroInicial { get; set; } // Nullable para manejar valores NULL en la base de datos
        public int? OdometroFinal { get; set; } // Nullable para manejar valores NULL en la base de datos
        public int IdReservacion { get; set; }
        public int? IdColaborador { get; set; } // Nullable si IdColaborador puede ser NULL
        public int? IdAccesorio { get; set; } // Nullable si IdAccesorio puede ser NULL
        public int? IdTipoInspeccion { get; set; } // Nullable si IdTipoInspeccion puede ser NULL

        [StringLength(255)]
        public string ImagenRuta { get; set; } = string.Empty;

        public Reservacion Reservacion { get; set; }
        public Colaborador? Colaborador { get; set; }
        public Accesorio? Accesorio { get; set; }
        public TipoInspeccion? TipoInspeccion { get; set; }
    }
}

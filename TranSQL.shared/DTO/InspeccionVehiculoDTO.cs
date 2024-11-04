using System;

namespace TranSQL.shared.DTO
{
    public class InspeccionVehiculoDTO
    {
        public int IdInspeccion { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int? OdometroInicial { get; set; } // Nullable para valores NULL
        public int? OdometroFinal { get; set; } // Nullable para valores NULL
        public string ImagenRuta { get; set; } = string.Empty; // Valor predeterminado
        public int IdReservacion { get; set; }
        public string? ColaboradorNombre { get; set; } // Nullable para valores NULL
        public string? AccesorioNombre { get; set; } // Nullable para valores NULL
        public string? TipoInspeccionNombre { get; set; } // Nullable para valores NULL
    }
}

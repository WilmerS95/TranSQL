using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class TipoInspeccion
    {
        [Key]
        public int IdTipoInspeccion { get; set; }

        [Required]
        [StringLength(75)]
        public string NombreTipoInsepccion { get; set; } = string.Empty;

        public TipoInspeccion(string nombreTipoInsepccion)
        {
            NombreTipoInsepccion = nombreTipoInsepccion ?? throw new ArgumentNullException(nameof(nombreTipoInsepccion));
        }

        public TipoInspeccion()
        {
        }
    }
}

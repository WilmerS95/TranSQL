using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class TipoInspeccion
    {
        private int IdTipoInspeccion {  get; set; }

        [Required]
        [StringLength(75)]
        private string NombreTipoInsepccion { get; set; } = string.Empty;

        public TipoInspeccion(string nombreTipoInsepccion)
        {
            NombreTipoInsepccion = nombreTipoInsepccion ?? throw new ArgumentNullException(nameof(nombreTipoInsepccion));
        }

        public TipoInspeccion()
        {
        }
    }
}

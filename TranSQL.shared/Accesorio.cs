using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Accesorio
    {
        private int IdAccesorio {  get; set; }

        [Required]
        [StringLength(75)]
        private string NombreAccesorio { get; set; } = string.Empty;

        [Required]
        [StringLength(75)]
        private string EstadoAccesorio { get; set; } = string.Empty;

        public Accesorio(int idAccesorio, string nombreAccesorio, string estadoAccesorio)
        {
            IdAccesorio = idAccesorio;
            NombreAccesorio = nombreAccesorio ?? throw new ArgumentNullException(nameof(nombreAccesorio));
            EstadoAccesorio = estadoAccesorio ?? throw new ArgumentNullException(nameof(estadoAccesorio));
        }

        public Accesorio()
        {
        }
    }
}

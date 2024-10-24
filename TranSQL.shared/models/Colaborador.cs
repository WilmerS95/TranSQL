using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.models
{
    public class Colaborador
    {
        [Key]
        public int IdColaborador { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimerNombre { get; set; } = string.Empty;

        [StringLength(50)]
        public string? SegundoNombre { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimerApellido { get; set; } = string.Empty;

        [StringLength(50)]
        public string? SegundoApellido { get; set; }

        [StringLength(50)]
        public string? ApellidoDeCasada { get; set; }

        [Required]
        [StringLength(75)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int IdDepartamento { get; set; }

        [ForeignKey(nameof(IdDepartamento))]
        public Departamento Departamento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Colaborador
    {
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
        public int IdDepartamento { get; set; }

        public Departamento departamento { get; set; }

        public Colaborador( string primerNombre, string? segundoNombre, string primerApellido, string? segundoApellido, string? apellidoDeCasada, string correo, int idDepartamento, Departamento departamento)
        {
            //IdColaborador = idColaborador;
            PrimerNombre = primerNombre ?? throw new ArgumentNullException(nameof(primerNombre));
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido ?? throw new ArgumentNullException(nameof(primerApellido));
            SegundoApellido = segundoApellido;
            ApellidoDeCasada = apellidoDeCasada;
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            IdDepartamento = idDepartamento;
            this.departamento = departamento ?? throw new ArgumentNullException(nameof(departamento));
        }

        public Colaborador()
        {
        }
    }
}

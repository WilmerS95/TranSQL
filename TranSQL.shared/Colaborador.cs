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
        private int IdColaborador { get; set; }

        [Required]
        [StringLength(50)]
        private string PrimerNombre { get; set; } = string.Empty;

        [StringLength(50)]
        private string? SegundoNombre { get; set; }

        [Required]
        [StringLength(50)]
        private string PrimerApellido { get; set; } = string.Empty;

        [StringLength(50)]
        private string? SegundoApellido { get; set; }

        [StringLength(50)]
        private string? ApellidoDeCasada { get; set; }

        [Required]
        [StringLength(75)]
        private string Correo { get; set; } = string.Empty;

        [Required]
        private int IdDepartamento { get; set; }

        private Departamento departamento { get; set; }

        public Colaborador(int idColaborador, string primerNombre, string? segundoNombre, string primerApellido, string? segundoApellido, string? apellidoDeCasada, string correo, int idDepartamento, Departamento departamento)
        {
            IdColaborador = idColaborador;
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

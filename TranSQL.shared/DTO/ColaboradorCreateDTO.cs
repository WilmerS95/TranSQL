using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class ColaboradorCreateDTO
    {
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? ApellidoDeCasada { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int IdDepartamento { get; set; }
    }
}

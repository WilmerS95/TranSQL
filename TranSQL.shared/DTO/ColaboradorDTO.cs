using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared.DTO
{
    public class ColaboradorDTO
    {
        public int IdColaborador { get; set; }
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranSQL.shared
{
    public class Departamento
    {

        // Id único para el departamento
        private int IdDepartamento { get; set; }

        //Porque no acepta valores nulos se agrega required o se inicializa con string.Empty
        // Campo requerido que no acepta valores nulos
        [Required]
        [StringLength(100)]  // Si quieres limitar el tamaño como en la base de datos
        private string NombreDepartamento { get; set; } = string.Empty;

        //Constructor vacío
        public Departamento()
        {
        }

        //Constructor que recibe solamente el nombre
        public Departamento(string nombreDepartamento)
        {
            NombreDepartamento = nombreDepartamento;
        }

        //Constructor que recibe idDepartamento y nombreDepartamento
        public Departamento(int idDepartamento, string nombreDepartamento)
        {
            IdDepartamento = idDepartamento;
            NombreDepartamento = nombreDepartamento;
        }
    }
}

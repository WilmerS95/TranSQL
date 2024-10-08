namespace TranSQL.Shared
{
    public class Departamento
    {
        public int idDepartamento;
        public string nombreDepartamento = string.Empty; // Initialize the field

        public Departamento() { }

        public Departamento(int idDepartamento, string nombreDepartamento)
        {
            this.IdDepartamento = idDepartamento;
            this.NombreDepartamento = nombreDepartamento; // Validation handled in the setter
        }

        public int IdDepartamento { get; set; }


        public string NombreDepartamento
        {
            get => nombreDepartamento;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("NombreDepartamento cannot be null or empty.");
                }
                nombreDepartamento = value;
            }
        }

        public override string ToString()
        {
            return $"Id: {IdDepartamento}, Nombre: {NombreDepartamento}";
        }
    }
}

using System.ComponentModel;

namespace App.Models
{
    public class Persona : BaseEntity
    {
        public string Identificación { get; set; }

        public Sexo Sexo { get; set; }

        public int Edad { get; set; }

        public string Nombres { get; set; }

        [DisplayName("Primer apellido")]
        public string PrimerApellido { get; set; }

        [DisplayName("Segundo apellido")]
        public string SegundoApellido { get; set; }

        public Persona() { }

        public string NombreCompleto()
        {
            return Nombres + " " + PrimerApellido + " " + SegundoApellido;
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Persona : BaseEntity
    {
        [DataType(DataType.PhoneNumber)]
        public string Identificación { get; set; }

        public Sexo Sexo { get; set; }

        public int Edad { get; set; }

        [DataType(DataType.Text, ErrorMessage = "EL nombre es inválido")]
        public string Nombres { get; set; }

        [DisplayName("Primer apellido")]
        [DataType(DataType.Text, ErrorMessage = "El primer apellido es inválido")]
        public string PrimerApellido { get; set; }

        [DisplayName("Segundo apellido")]
        [DataType(DataType.Text, ErrorMessage = "El segundo apellido es inválido")]
        public string SegundoApellido { get; set; }

        public Persona() { }

        public string NombreCompleto()
        {
            return Nombres + " " + PrimerApellido + " " + SegundoApellido;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Usuario : Persona
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(16, ErrorMessage = "La longitud máxima para el nombre es de 16")]
        [MinLength(6, ErrorMessage = "La longitud mínima para el nombre es de 6")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MaxLength(16, ErrorMessage = "La longitud máxima de la contraseña es de 16")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la contrasea es de 6")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        public string CorreoElectrónico { get; set; }

        [NotMapped]
        [DisplayName("Confirmación contraseña")]
        public string ConfirmaciónContraseña { get; set; }

        public Rol Rol { get; set; }

        public ICollection<Sesión> Sesiones { get; set; }

        public Usuario()
        {
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Sesión
    {
        [NotMapped]
        [Required(ErrorMessage="El nombre es requerido")]
        [MaxLength(20, ErrorMessage ="El nombre es muy largo")]
        [MinLength(10, ErrorMessage="El nombre es muy corto")]
        public string Nombre { get; set; }

        [NotMapped]
        [Required(ErrorMessage="La contraseña es requerida")]
        [MaxLength(16, ErrorMessage="Ingrese una contraseña válida")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        public long Id { get; set; }

        public Usuario Usuario { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public SesiónState Estado { get; set; }

        public Sesión()
        {
        }
    }
}
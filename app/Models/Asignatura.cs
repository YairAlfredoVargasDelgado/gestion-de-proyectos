using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Asignatura : BaseEntity
    {
        [MaxLength(10, ErrorMessage = "Este código es demasiado largo")]
        [MinLength(5, ErrorMessage = "Este código es demasiado corto")]
        [Required]
        public string Código { get; set; }

        public string Nombre { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; }

        public Asignatura()
        {
            Proyectos = new HashSet<Proyecto>();
        }
    }
}
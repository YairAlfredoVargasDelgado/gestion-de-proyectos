using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Proyecto : BaseEntity
    {
        public string Código { get; set; }

        public string Nombre { get; set; }

        [DisplayName("Asignatura")]
        [NotMapped]
        public string IdAsignatura { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [MinLength(20, ErrorMessage = "La descripción es inválida")]
        public string Descripción { get; set; }

        public Asignatura Asignatura { get; set; }

        public ICollection<EstudianteProyecto> ProyectoEstudiantes { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [MinLength(6, ErrorMessage = "La cédula es inválida")]
        [DisplayName("Cédula del primer calificador")]
        [NotMapped]
        public string IdDirector { get; set; }

        public Director Director { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [MinLength(6, ErrorMessage = "La cédula es inválida")]
        [DisplayName("Cédula del director")]
        [NotMapped]
        public string IdCalificado1 { get; set; }

        public Calificador Calificador1 { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [MinLength(6, ErrorMessage = "La cédula es inválida")]
        [DisplayName("Cédula del segundo calificador")]
        [NotMapped]
        public string IdCalificado2 { get; set; }

        public Calificador Calificador2 { get; set; }

        [DisplayName("Rúbrica de calificación")]
        [NotMapped]
        public long IdRúbrica { get; set; }

        public Rúbrica Rúbrica { get; set; }

        public Proyecto()
        {
        }
    }
}
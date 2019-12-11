using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class EstudianteProyecto
    {
        public long IdEstudiante { get; set; }

        public Estudiante Estudiante { get; set; }

        public long IdProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        public EstudianteProyecto()
        {
        }
    }
}
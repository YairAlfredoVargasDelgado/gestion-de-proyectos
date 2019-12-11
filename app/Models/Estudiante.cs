using System.Collections.Generic;

namespace App.Models
{
    public class Estudiante : Persona
    {
        public ICollection<EstudianteProyecto> EstudianteProyectos { get; set; }

        public Estudiante()
        {
        }
    }
}
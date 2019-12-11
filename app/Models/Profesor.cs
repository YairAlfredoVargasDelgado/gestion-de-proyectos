using System.Collections.Generic;

namespace App.Models
{
    public class Profesor : Usuario
    {
        public ICollection<Asignatura> Asignaturas { get; set; }

        public Profesor()
        {
            Asignaturas = new HashSet<Asignatura>();
        }
    }
}
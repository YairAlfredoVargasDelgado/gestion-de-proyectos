using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Calificador : Profesor
    {
        public ICollection<ProyectoCalificador> CalificadorProyectos { get; set; }

        [NotMapped]
        public ICollection<Proyecto> Proyectos { get; set; }

        public Calificador()
        {
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ProyectoCalificador
    {
        public long IdProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        public long IdCalificador { get; set; }

        public Calificador Calificador { get; set; }

        public ProyectoCalificador()
        {
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Criterio : BaseEntity
    {
        public string Descripción { get; set; }

        public int Porcentaje { get; set; }
        
        [NotMapped]
        public long IdRúbrica { get; set; }

        public Rúbrica Rúbrica { get; set; }

        public Criterio() { }
    }
}
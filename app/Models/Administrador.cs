using System.Collections.Generic;

namespace App.Models
{
    public class Administrador : Usuario
    {
        public ICollection<Rúbrica> Rúbricas { get; set; }

        public Administrador() { }
    }
}
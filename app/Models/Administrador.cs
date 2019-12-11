using System.Collections.Generic;

namespace App.Models
{
    public class Administrador : BaseEntity
    {
        public ICollection<Rúbrica> Rúbricas { get; set; }

        public Administrador() { }
    }
}
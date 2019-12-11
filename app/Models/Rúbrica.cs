using System;
using System.Collections.Generic;

namespace App.Models
{
    public class Rúbrica : BaseEntity
    {
        public DateTime FechaDeRegistro { get; set; }

        public string Nombre { get; set; }

        public RúbricaState Estado {get;set;}

        public ICollection<Criterio> Criterios { get; set; }

        public Rúbrica()
        {
            Criterios = new HashSet<Criterio>();
        }

        public enum RúbricaState
        {
            ACTIVA,
            INACTIVA
        }
    }
}
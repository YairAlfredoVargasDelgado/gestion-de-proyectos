using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Rúbrica : BaseEntity
    {
        [DisplayName("Fecha de registro")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaDeRegistro { get; set; }

        [MinLength(10, ErrorMessage = "El nombre debe tener al menos 10 caracteres")]
        [MaxLength(45)]
        public string Nombre { get; set; }

        public RúbricaState Estado { get; set; }

        [DisplayName("Calificación máxima")]
        public int CalificaciónMáxima { get; set; }

        [DisplayName("Porcentaje restante")]
        [NotMapped]
        public int PorcentajeRestante { get; set; }

        public ICollection<Criterio> Criterios { get; set; }

        public Rúbrica()
        {
            Criterios = new HashSet<Criterio>();
        }

        public void CalcularPorcentajeRestante()
        {
            int porcentajeRestante = 100;
            foreach (var c in Criterios)
            {
                porcentajeRestante -= c.Porcentaje;
            }
            PorcentajeRestante = porcentajeRestante;
        }

        public enum RúbricaState
        {
            ACTIVA,
            INACTIVA
        }
    }
}
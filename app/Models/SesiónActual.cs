using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [NotMapped]
    public static class SesiónActual
    {
        public static Sesión Sesión { get; set; }

    }
}
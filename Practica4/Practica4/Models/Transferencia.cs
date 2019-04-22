using System.ComponentModel.DataAnnotations;

namespace Practica4.Models
{
    public class Transferencia
    {
        [Required]
        public int cuenta1 { get; set; }

        [Required]
        public int cuenta2 { get; set; }

        [Required]
        public float monto { get; set; }
    }
}
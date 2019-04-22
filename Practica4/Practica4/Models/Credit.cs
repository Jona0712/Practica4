using System.ComponentModel.DataAnnotations;

namespace Practica4.Models
{
    public class Credit
    {
        [Required]
        public float monto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int cuenta { get; set; }

        [Required]
        public int estado { get; set; }
    }
}
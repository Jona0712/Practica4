using System.ComponentModel.DataAnnotations;

namespace Practica4.Models
{
    public class Login
    {
        [Required]
        public int codigo { get; set; }

        [Required]
        public string usua { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string pass { get; set; }
    }
}
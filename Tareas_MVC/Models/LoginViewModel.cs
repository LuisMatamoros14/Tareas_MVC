using System.ComponentModel.DataAnnotations;

namespace Tareas_MVC.Models
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electronico valido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Recuerdame")]
        public bool Recuerdame { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Tareas_MVC.Models
{
    public class RegistroViewModel
    {
        [EmailAddress(ErrorMessage ="Error.Email")]
        [Required(ErrorMessage ="Error.Requerido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage ="Error.Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace Tareas_MVC.Servicios
{
    public class Constantes
    {
        public const string RolAdmin = "admin";
        public static readonly SelectListItem[] CulturasUISoportadas = new SelectListItem[]
        {
            new SelectListItem{Value = "es", Text="Espanol"},
            new SelectListItem{Value = "en", Text = "English"},
        };
    }
}

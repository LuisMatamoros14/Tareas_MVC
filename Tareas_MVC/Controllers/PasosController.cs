using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tareas_MVC.Entidades;
using Tareas_MVC.Models;
using Tareas_MVC.Servicios;

namespace Tareas_MVC.Controllers
{
    [Route("api/pasos")]
    public class PasosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioUsuarios servicioUsuarios;

        public PasosController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios)
        {
            this.context = context;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpPost("{tareaId:int}")]
        public async Task<ActionResult<Paso>> Post(int tareaId, [FromBody] PasoCrearDTO pasoCrearDTO)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tarea = await context.Tareas.FirstOrDefaultAsync(t => t.Id == tareaId);
            
            if (tarea == null)
            {
                return NotFound();
            }

            if(tarea.UsuarioCreacionId != usuarioId)
            {
                return Forbid();
            }

            //verifica si existe un paso en la tabla
            var existenPasos = await context.Pasos.AnyAsync(p=>p.TareaId ==tareaId);

            var ordenMayor = 0;
            if (existenPasos)
            {
                ordenMayor = await context.Pasos.Where(p => p.TareaId == tareaId).Select(p=>p.Orden).MaxAsync();
            }

            var paso = new Paso();
            paso.TareaId = tareaId;
            paso.Descripcion = pasoCrearDTO.Descripcion;
            paso.Orden = ordenMayor;
            paso.Realizado = pasoCrearDTO.Realizado;

            context.Add(paso);
            await context.SaveChangesAsync();

            return paso;
        }
    }
}

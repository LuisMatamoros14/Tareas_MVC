﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tareas_MVC.Entidades;
using Tareas_MVC.Servicios;

namespace Tareas_MVC.Controllers
{
    [Route("api/tareas")]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioUsuarios servicioUsuarios;

        public TareasController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios)
        {
            this.context = context;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<List<Tarea>> Get()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            return await context.Tareas.Where(t=>t.UsuarioCreacionId==usuarioId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var existenTareas = await context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);

            var ordenMayor = 0;
            
            if(existenTareas)
            {
                ordenMayor = await context.Tareas.Where(t=>t.UsuarioCreacionId == usuarioId).Select(t=> t.Orden).MaxAsync();
            }

            var tarea = new Tarea()
            {
                Titulo = titulo,
                UsuarioCreacionId = usuarioId,
                FechaCreacion = DateTime.UtcNow,
                Orden = ordenMayor + 1
            };

            context.Add(tarea);
            await context.SaveChangesAsync();

            return tarea;
        }

    }
}
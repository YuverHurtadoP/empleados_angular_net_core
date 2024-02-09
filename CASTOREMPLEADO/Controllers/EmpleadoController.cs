using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CASTOREMPLEADO.Entity;

using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace CASTOREMPLEADO.Controllers
{
    [EnableCors("misReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        public readonly CastorContext _dbcastorContext;
        public EmpleadoController(CastorContext _context)
        {
            _dbcastorContext = _context;
            
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult lista() { 
            List<Empleado> lista = new List<Empleado>();
            try {
                lista = _dbcastorContext.Empleados.Include(c => c.Cargo).ToList();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "ok", response = lista });
            
            }catch(Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "err", response = ex});
            }
        }


        [HttpGet]
        [Route("obtener/{cedula:int}")]
        public IActionResult obtenerEmpleado(int cedula)
        {
            Empleado empleado = _dbcastorContext.Empleados.Include(c => c.Cargo).Where(cd => cd.Cedula == cedula).FirstOrDefault();
            if (empleado == null)
            {
                return BadRequest("Empleado no encontrado");
            }
            try
            {
                
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = empleado });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "err", response = ex });
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult guardarEmpleado([FromBody] Empleado empleado)
        {
            Empleado empleadoExistente = _dbcastorContext.Empleados
                .Where(e => e.Cedula == empleado.Cedula)
                .FirstOrDefault();

            if (empleadoExistente != null)
            {
                return BadRequest(new { mensaje = "Empleado ya existe en la base de datos" });
            }

            try
            {
                _dbcastorContext.Empleados.Add(empleado);
                _dbcastorContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor", response = ex.Message });
            }
        }


        [HttpPut]
        [Route("actualizar")]
        public IActionResult actualizarEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                // Buscar el empleado existente sin rastrearlo
                var empleadoExistente = _dbcastorContext.Empleados.AsNoTracking().FirstOrDefault(e => e.Id == empleado.Id);

                if (empleadoExistente == null)
                {
                    return BadRequest(new { mensaje = "Empleado no existe en la base de datos" });
                }

                // Desvincular la entidad rastreada actual del contexto
                _dbcastorContext.Entry(empleadoExistente).State = EntityState.Detached;

                // Actualizar el empleado
                _dbcastorContext.Empleados.Update(empleado);
                _dbcastorContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor", response = ex.Message });
            }
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminar (int id)
        {
            Empleado empleadoExistente = _dbcastorContext.Empleados.Where(e => e.Id == id).FirstOrDefault();

            if (empleadoExistente == null)
            {
                return BadRequest("Empleado no encontrado");
            }
            try
            {
                _dbcastorContext.Empleados.Remove(empleadoExistente);
                _dbcastorContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "err", response = ex });
            }
        }

    }
}

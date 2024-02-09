using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using Modelos;
using CASTOREMPLEADO.DBContext;
using CASTOREMPLEADO.DAL.Interfaces;

namespace CASTOREMPLEADO.Controllers
{
    [EnableCors("misReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly IEmpleadoRepositorio _empleadoRepositorio;

        public EmpleadoController(IEmpleadoRepositorio empleadoRepositorio)
        {
            _empleadoRepositorio = empleadoRepositorio;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult Lista()
        {
            try
            {
                var lista = _empleadoRepositorio.ObtenerTodos();
                return Ok(new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }

        [HttpGet]
        [Route("obtener/{cedula:int}")]
        public IActionResult ObtenerEmpleado(int cedula)
        {
            try
            {
                var empleado = _empleadoRepositorio.ObtenerPorCedula(cedula);

                if (empleado == null)
                    return BadRequest(new { mensaje = "Empleado no encontrado" });

                return Ok(new { mensaje = "ok", response = empleado });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }



        [HttpPost]
        [Route("guardar")]
        public IActionResult GuardarEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                if (_empleadoRepositorio.ExisteCedula(empleado.Cedula)) {
                    return BadRequest(new { mensaje = "Ya existe un empleado con la misma cédula." });

                }
                _empleadoRepositorio.Guardar(empleado);
                return Ok(new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }


        [HttpPut]
        [Route("actualizar")]
        public IActionResult ActualizarEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                _empleadoRepositorio.Actualizar(empleado);
                return Ok(new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _empleadoRepositorio.Eliminar(id);
                return Ok(new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }
        /*
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
                }*/

    }
}

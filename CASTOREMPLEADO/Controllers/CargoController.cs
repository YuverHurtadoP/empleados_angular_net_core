using CASTOREMPLEADO.DAL.implementaciones;
using CASTOREMPLEADO.DAL.Interfaces;
using CASTOREMPLEADO.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace CASTOREMPLEADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        public readonly ICargoRepositorio _cargoRepositorio;
        public CargoController(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult lista()
        { 
            try
            {
                var lista = _cargoRepositorio.ObtenerTodos();
                return Ok(new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "err", response = ex.Message });
            }
        }
    }
}

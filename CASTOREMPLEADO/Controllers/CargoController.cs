using CASTOREMPLEADO.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASTOREMPLEADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        public readonly CastorContext _dbcastorContext;
        public CargoController(CastorContext _context)
        {
            _dbcastorContext = _context;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult lista()
        {
            List<Cargo> lista = new List<Cargo>();
            try
            {
                lista = _dbcastorContext.Cargos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "err", response = ex });
            }
        }
    }
}

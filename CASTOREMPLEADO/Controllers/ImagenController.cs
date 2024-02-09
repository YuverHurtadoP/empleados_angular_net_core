using CASTOREMPLEADO.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASTOREMPLEADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {

        private readonly string _imageUploadPath = "Imgs";
        private readonly string _carpetaImagenes;

        public ImagenController()
        {
            // Especifica la carpeta donde se almacenan las imágenes
            _carpetaImagenes = Path.Combine(Directory.GetCurrentDirectory(), "Imgs");
        }


        [HttpPost]
        [Route("subir")]
        public IActionResult subirImagen(IFormFile foto)
        {
            try
            {
                if (foto == null || foto.Length == 0)
                {
                    return BadRequest("Archivo no proporcionado o está vacío.");
                }

                // Genera un nombre único para el archivo
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);


                // Obtiene la ruta de la carpeta donde se guardarán las imágenes
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), _imageUploadPath, fileName);
                Console.WriteLine(imagePath);

                // Guarda el archivo en la carpeta
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    foto.CopyTo(stream);
                }
                var imagenResponse = new Dtos.Response.ImagenResponse
                {
                    FileName = fileName,
                    FilePath = imagePath
                };

                return Ok(imagenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("{nombreImagen}")]
        public IActionResult ObtenerImagen(string nombreImagen)
        {
            var rutaImagen = Path.Combine(_carpetaImagenes, nombreImagen);
           
            if (System.IO.File.Exists(rutaImagen))
            {
                // Devuelve la imagen
                return PhysicalFile(rutaImagen, "image/png"); // Ajusta el tipo MIME según el formato de tus imágenes
            }

            // Devuelve un error 404 si la imagen no se encuentra
            return NotFound();
        }


    }
}
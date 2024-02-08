// ImageController.cs
using CASTOREMPLEADO.Dto.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

[EnableCors("ReglasCors")]
[Route("api/[controller]")]
[ApiController]
public class ImagenController : ControllerBase
{
    private readonly string _imageUploadPath = "Imgs";

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

            var imageModel = new ImageModel
            {
                FileName = fileName,
                FilePath = imagePath
            };

            return Ok(imageModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

 
}

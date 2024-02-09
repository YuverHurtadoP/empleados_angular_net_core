using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D

namespace CASTOREMPLEADO.BLL.Interfaces
{
    public interface IImagenService
    {
        Task<ImagenResponseDto> SubirImagenAsync(IFormFile foto);
        Task<IActionResult> ObtenerImagenAsync(string nombreImagen);
    }
}

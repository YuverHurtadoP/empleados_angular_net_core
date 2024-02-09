using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASTOREMPLEADO.DAL.Interfaces
{
    public interface ICargoRepositorio
    {
        IEnumerable<Cargo> ObtenerTodos();
    }
}

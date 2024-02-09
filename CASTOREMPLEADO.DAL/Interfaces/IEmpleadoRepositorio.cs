using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASTOREMPLEADO.DAL.Interfaces
{
    public interface IEmpleadoRepositorio
    {
        IEnumerable<Empleado> ObtenerTodos();
        Empleado ObtenerPorCedula(int cedula);
        void Guardar(Empleado empleado);
        void Actualizar(Empleado empleado);
        void Eliminar(int id);
        bool ExisteCedula(int cedula);
    }
}

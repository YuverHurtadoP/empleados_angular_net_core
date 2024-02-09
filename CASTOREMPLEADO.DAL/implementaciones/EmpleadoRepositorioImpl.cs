using CASTOREMPLEADO.DAL.Interfaces;
using CASTOREMPLEADO.DBContext;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASTOREMPLEADO.DAL.implementaciones
{
    public class EmpleadoRepositorioImpl: IEmpleadoRepositorio
    {
        private readonly CastorContext _dbcastorContext;

        public EmpleadoRepositorioImpl(CastorContext context)
        {
            _dbcastorContext = context;
        }

        public IEnumerable<Empleado> ObtenerTodos()
        {
            try
            {
                return _dbcastorContext.Empleados.Include(c => c.Cargo).ToList();
            }
            catch (Exception ex)
            {
             
                throw;
            }
        }

        public Empleado ObtenerPorCedula(int cedula)
        {
            try
            {
                return _dbcastorContext.Empleados.Include(c => c.Cargo).FirstOrDefault(cd => cd.Cedula == cedula);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void Guardar(Empleado empleado)
        {
            try
            {
                _dbcastorContext.Empleados.Add(empleado);
                _dbcastorContext.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void Actualizar(Empleado empleado)
        {
            try
            {
               
                _dbcastorContext.Entry(empleado).State = EntityState.Modified;
                _dbcastorContext.SaveChanges();
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                
                var empleado = _dbcastorContext.Empleados.Find(id);
                if (empleado != null)
                {
                    _dbcastorContext.Empleados.Remove(empleado);
                    _dbcastorContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
              
                throw;
            }
        }

        public bool ExisteCedula(int cedula)
        {
            return _dbcastorContext.Empleados.Any(e => e.Cedula == cedula);
        }
    }
}
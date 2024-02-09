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
    public class CargoRepositorioImpl : ICargoRepositorio
    {

        private readonly CastorContext _dbcastorContext;

        public CargoRepositorioImpl(CastorContext context)
        {
            _dbcastorContext = context;
        }
        IEnumerable<Cargo> ICargoRepositorio.ObtenerTodos()
        {
            try
            {
                return _dbcastorContext.Cargos.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

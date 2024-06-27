using Infrastructure.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IVehiculoRepository : IRepository<VehiculoEntity>
    {
        // metodos propios de este repositorio
    }
}

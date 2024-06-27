using Infrastructure.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IVehiculoServicio
    {
        IEnumerable<VehiculoEntity> GetAll();
        //VehiculoEntity Get(int id);
        //void Create(VehiculoEntity model);
        //void Update(VehiculoEntity model);
        //void Delete(int id);
    }

    public class VehiculoServicio : IVehiculoServicio
    {
        private ISQLServerIndusur _repository;

        public VehiculoServicio(ISQLServerIndusur repo) 
        {
            _repository = repo;
        }

        public IEnumerable<VehiculoEntity> GetAll()
        {
            using (var context = _repository.Create())
            {
                var records = context.repositorioVehiculo.VehiculoRepository.GetAll();

                return records;
            }
        }
    }
}

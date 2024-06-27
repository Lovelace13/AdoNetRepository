using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entidades
{
    public class VehiculoEntity
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string chasis { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int anio_modelo { get; set; }
        public string color { get; set; }
        public string estado { get; set; }
        public DateTime fecha_registro { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHV.Models
{
    public class Permiso
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreGuardian { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public string Descripcion { get; set; }
        public string Modelo { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudConsumoMiniMarket.Models
{
    public class Proveedores
    {
        public int IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public  ICollection<Productos> Productos { get; set; }
    }
}
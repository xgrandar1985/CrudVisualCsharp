using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudConsumoMiniMarket.Models
{
    public class Productos
    {
        public int IdProducto { get; set; }

        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        [Required]
        public int IdProveedor { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Medida { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }

        public  Categorias Categorias { get; set; }
        public  Proveedores Proveedores { get; set; }
    }
}
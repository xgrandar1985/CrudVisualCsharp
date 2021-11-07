using System.ComponentModel.DataAnnotations;

namespace InventarioProductosWebApi.Models.Validaciones
{
    [MetadataType(typeof(Productos.MetaData))]
    public partial class Productos
    {
        sealed class MetaData
        {
            [Required]
            public string Codigo;

            [Required]
            public string Descripcion;

            [Required]
            public int IdCategoria;

            [Required]
            public int IdProveedor;

            [Required]
            public string Marca;

            [Required]
            public string Medida;

            [Required]
            public int Cantidad;

            [Required]
            public decimal PrecioUnitario;
        }
    }
}
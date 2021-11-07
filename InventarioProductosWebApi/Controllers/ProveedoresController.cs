using InventarioProductosWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace InventarioProductosWebApi.Controllers
{
    public class ProveedoresController : ApiController
    {
        private readonly MiniMarketDbEntities db = new MiniMarketDbEntities();

        // GET: api/Proveedores
        public IQueryable<Proveedores> GetProveedores()
        {
            List<Proveedores> lstProveedores = null;
            var objProveedores = db.spObtenerProveedores();
            lstProveedores = (from item in objProveedores
                              select new Proveedores
                              {
                                  IdProveedor = item.IdProveedor,
                                  Descripcion = item.Descripcion,
                                  Estado = item.Estado
                                  //Productos = new List<Productos>()
                              }).ToList();
            return lstProveedores.AsQueryable();
        }

        // GET: api/Proveedores/5
        [ResponseType(typeof(Proveedores))]
        public IHttpActionResult GetProveedores(int id)
        {
            var objProv = db.spObtenerProveedoresPorId(id);
            var proveedor = (from item in objProv
                             select new Proveedores
                             {
                                 IdProveedor = item.IdProveedor,
                                 Descripcion = item.Descripcion,
                                 Estado = item.Estado
                                 //Productos = new List<Productos>()
                             }).FirstOrDefault();
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }
    }
}

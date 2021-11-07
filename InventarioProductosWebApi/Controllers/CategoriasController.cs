using InventarioProductosWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace InventarioProductosWebApi.Controllers
{
    public class CategoriasController : ApiController
    {
        private readonly MiniMarketDbEntities db = new MiniMarketDbEntities();

        // GET: api/Categorias
        public IQueryable<Categorias> GetCategorias()
        {
            List<Categorias> lstCategorias = null;
            var objCategorias = db.spObtenerCategorias();
            lstCategorias = (from item in objCategorias
                             select new Categorias
                             {
                                 Id = item.Id,
                                 Descripcion = item.Descripcion,
                                 Estado = item.Estado
                                 //Productos = new List<Productos>()
                             }).ToList();
            return lstCategorias.AsQueryable();
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Categorias))]
        public IHttpActionResult GetCategorias(int id)
        {
            var objCat = db.spObtenerCategoriasPorId(id);
            var categorias = (from item in objCat
                              select new Categorias
                              {
                                  Id = item.Id,
                                  Descripcion = item.Descripcion,
                                  Estado = item.Estado
                                  //Productos = new List<Productos>()
                              }).FirstOrDefault();
            if (categorias == null)
            {
                return NotFound();
            }
            return Ok(categorias);
        }
    }
}

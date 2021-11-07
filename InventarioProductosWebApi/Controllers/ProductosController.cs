﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InventarioProductosWebApi.Models;

namespace InventarioProductosWebApi.Controllers
{
    public class ProductosController : ApiController
    {
        private readonly MiniMarketDbEntities db = new MiniMarketDbEntities();

        // GET: api/Productos
        public IQueryable<Productos> GetProductos()
        {
            List<Productos> lstProductos = null;
            var objProductos = db.spObtenerProductos();
            lstProductos = (from item in objProductos
                            select new Productos
                            {
                                 IdProducto = item.IdProducto,
                                 Codigo = item.Codigo,
                                 Descripcion = item.Descripcion,
                                 Cantidad = item.Cantidad,
                                 IdCategoria = item.IdCategoria,
                                 IdProveedor = item.IdProveedor,
                                 Marca = item.Marca,
                                 Medida = item.Medida,
                                 PrecioUnitario  = item.PrecioUnitario,
                                 Categorias = new Categorias() { Id = item.IdCategoria,Descripcion = item.Categoria },
                                 Proveedores = new Proveedores() { IdProveedor = item.IdProveedor, Descripcion = item.Proveedor }
                            }).ToList();
            return lstProductos.AsQueryable();
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(int id)
        {
            var objProductos = db.spObtenerProductoPorId(id);
            var productos = (from item in objProductos
                            select new Productos
                            {
                                IdProducto = item.IdProducto,
                                Codigo = item.Codigo,
                                Descripcion = item.Descripcion,
                                Cantidad = item.Cantidad,
                                IdCategoria = item.IdCategoria,
                                IdProveedor = item.IdProveedor,
                                Marca = item.Marca,
                                Medida = item.Medida,
                                PrecioUnitario = item.PrecioUnitario,
                                Categorias = new Categorias() { Id = item.IdCategoria, Descripcion = item.Categoria },
                                Proveedores = new Proveedores() { IdProveedor = item.IdProveedor, Descripcion = item.Proveedor }
                            }).FirstOrDefault();

            if (productos == null)
            {
                return NotFound();
            }

            return Ok(productos);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductos(int id, Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productos.IdProducto)
            {
                return BadRequest();
            }

            db.Entry(productos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(productos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productos.IdProducto }, productos);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult DeleteProductos(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }

            db.Productos.Remove(productos);
            db.SaveChanges();

            return Ok(productos);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool ProductosExists(int id)
        {
            return db.Productos.Count(e => e.IdProducto == id) > 0;
        }

        
    }
}
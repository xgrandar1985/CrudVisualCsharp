using CrudConsumoMiniMarket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CrudConsumoMiniMarket.Controllers
{
    public class ProductosController : Controller
    {
       
        private const string V = "http://localhost:50736/";
        readonly string BaseUrl = V;

        public async Task<ActionResult> Index()
        {
            List<Productos> lstReg = new List<Productos>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage resp = await client.GetAsync("api/Productos");
                if (resp.IsSuccessStatusCode)
                {
                    var lstResponse = resp.Content.ReadAsStringAsync().Result;
                    lstReg = JsonConvert.DeserializeObject<List<Productos>>(lstResponse);
                }
                return View(lstReg);
            }       
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Productos obj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Todos los campos son requeridos.");
                return View(obj);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl + "api/Productos");
                    var postTask = client.PostAsJsonAsync<Productos>("Productos", obj);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrio un error al guardar los datos.");
                        return View(obj);
                    }
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(obj);
            }
        }


        public ActionResult Editar(int id)
        {
            Productos prod = new Productos();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var respTask = client.GetAsync("api/Productos/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Productos>();
                    readTask.Wait();
                    prod = readTask.Result;
                }
                return View(prod);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Productos obj)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Todos los campos son requeridos.");
                return View(obj);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var putTask = client.PutAsJsonAsync("api/Productos/" + obj.IdProducto.ToString(), obj);
                    putTask.Wait();
                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Ocurrio un error al actualizar el registro.");
                return View(obj);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(obj);
            }

        }

        public ActionResult Eliminar(int? id)
        {
            Productos prov = new Productos();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var respTask = client.GetAsync("api/Productos/" + id.Value.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Productos>();
                    readTask.Wait();
                    prov = readTask.Result;
                }
                return View(prov);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(Productos obj,int id)
        {
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var deleteTask = client.DeleteAsync("api/Productos/" + id.ToString());
                    deleteTask.Wait();
                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(obj);
        }


        public JsonResult ObtenerCategorias()
        {
            List<Categorias> lstReg = new List<Categorias>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var respTask = client.GetAsync("api/Categorias");
                respTask.Wait();
                HttpResponseMessage resp = respTask.Result;
                if (resp.IsSuccessStatusCode)
                {
                    var lstResponse = resp.Content.ReadAsStringAsync().Result;
                    lstReg = JsonConvert.DeserializeObject<List<Categorias>>(lstResponse);
                }
            }
            return Json(lstReg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerProveedores()
        {
            List<Proveedores> lstReg = new List<Proveedores>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var respTask = client.GetAsync("api/Proveedores");
                respTask.Wait();
                HttpResponseMessage resp = respTask.Result;
                if (resp.IsSuccessStatusCode)
                {
                    var lstResponse = resp.Content.ReadAsStringAsync().Result;
                    lstReg = JsonConvert.DeserializeObject<List<Proveedores>>(lstResponse);
                }
            }
            return Json(lstReg, JsonRequestBehavior.AllowGet);
        }

    }
}
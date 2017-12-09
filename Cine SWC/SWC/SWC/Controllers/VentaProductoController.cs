using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWC.Models;

namespace SWC.Controllers
{
    public class VentaProductoController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();

        // GET: VentaProducto
        public ActionResult Index()
        {
            var tb_VentaProducto = db.tb_VentaProducto.Include(t => t.tb_empleado);
            return View(tb_VentaProducto.ToList());
        }
        public ActionResult VentaProductosActuales()
        {
            var tb_VentaProductos = db.tb_VentaProducto;
            var lista = tb_VentaProductos.ToList().Where(m => ((DateTime)m.fecha).Day.Equals(DateTime.Now.Day));
            return View(lista);
        }
        public ActionResult VentaProductosxMes(int mes = 0, int mesFinal = 0)
        {
            ViewBag.mes = mes;
            ViewBag.mesFinal = mesFinal;
            var tb_VentaProductos = db.tb_VentaProducto;
            if(mes == 0 || mesFinal == 0)
            {
                return View(tb_VentaProductos.ToList());
            }
            else
            {
                if(mes>= mesFinal)
                {
                    ViewBag.mensaje = "el mes inicial no puede ser mayor o igual al mes final";
                }
                var lista = tb_VentaProductos.ToList().Where(m => ((DateTime)m.fecha).Month >= mes && ((DateTime)m.fecha).Month <= mesFinal);
                return View(lista);
            }
        }

        // GET: VentaProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaProducto tb_VentaProducto = db.tb_VentaProducto.Find(id);
            if (tb_VentaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tb_VentaProducto);
        }

        // GET: VentaProducto/Create
        public ActionResult Create()
        {
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre");
            ViewBag.idTipopro = new SelectList(db.tb_tipoProducto, "idTipopro", "desTipopro");
            ViewBag.idProducto = new SelectList(db.tb_producto, "idProducto", "nombre");
            ViewBag.idTamaño = new SelectList(db.tb_tamaño, "idTamaño", "desTamaño");
            return View();
        }

        // GET: VentaProducto/Create
        public ActionResult CreateP()
        {
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre");
            ViewBag.idTipopro = new SelectList(db.tb_tipoProducto, "idTipopro", "desTipopro");
            ViewBag.idProducto = new SelectList(db.tb_producto, "idProducto", "nombre");
            ViewBag.idTamaño = new SelectList(db.tb_tamaño, "idTamaño", "desTamaño");
            return View();
        }


        // GET: VentaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaProducto tb_VentaProducto = db.tb_VentaProducto.Find(id);
            if (tb_VentaProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaProducto.idEmpleado);
            return View(tb_VentaProducto);
        }

        // POST: VentaProducto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVentaP,fecha,idEmpleado,total")] tb_VentaProducto tb_VentaProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_VentaProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaProducto.idEmpleado);
            return View(tb_VentaProducto);
        }

        // GET: VentaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaProducto tb_VentaProducto = db.tb_VentaProducto.Find(id);
            if (tb_VentaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tb_VentaProducto);
        }

        // POST: VentaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_VentaProducto tb_VentaProducto = db.tb_VentaProducto.Find(id);
            db.tb_VentaProducto.Remove(tb_VentaProducto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult save(tb_VentaProducto ventaProducto)
        {
            bool status = false;

            var isValidModel = TryUpdateModel(ventaProducto);
            if (isValidModel)
            {
                using (db)
                {
                    ventaProducto.fecha = DateTime.Now;
                    ventaProducto.idEmpleado = (Int32)Session["idUsuario"];
                    db.tb_VentaProducto.Add(ventaProducto);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}

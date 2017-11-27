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
    public class VentaBoletosController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();

        // GET: VentaBoletos
        public ActionResult Index()
        {
            var tb_VentaBoleto = db.tb_VentaBoleto.Include(t => t.tb_cliente).Include(t => t.tb_empleado).Include(t => t.tb_funcion);
            return View(tb_VentaBoleto.ToList());
        }

        // GET: VentaBoletos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaBoleto tb_VentaBoleto = db.tb_VentaBoleto.Find(id);
            if (tb_VentaBoleto == null)
            {
                return HttpNotFound();
            }
            return View(tb_VentaBoleto);
        }

        // GET: VentaBoletos/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.tb_cliente, "idCliente", "nombre");
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre");
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion");
            return View();
        }

        // POST: VentaBoletos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVentaB,fecha,idFuncion,idEmpleado,idCliente,cantidad,total")] tb_VentaBoleto tb_VentaBoleto)
        {
            if (ModelState.IsValid)
            {
                tb_VentaBoleto.idEmpleado = (Int32)Session["idUsuario"];
                tb_VentaBoleto.fecha = DateTime.Today;
                db.tb_VentaBoleto.Add(tb_VentaBoleto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.tb_cliente, "idCliente", "nombre", tb_VentaBoleto.idCliente);
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaBoleto.idEmpleado);
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion", tb_VentaBoleto.idFuncion);
            return View(tb_VentaBoleto);
        }

        // GET: VentaBoletos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaBoleto tb_VentaBoleto = db.tb_VentaBoleto.Find(id);
            if (tb_VentaBoleto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.tb_cliente, "idCliente", "nombre", tb_VentaBoleto.idCliente);
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaBoleto.idEmpleado);
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion", tb_VentaBoleto.idFuncion);
            return View(tb_VentaBoleto);
        }

        // POST: VentaBoletos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVentaB,fecha,idFuncion,idEmpleado,idCliente,cantidad,total")] tb_VentaBoleto tb_VentaBoleto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_VentaBoleto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.tb_cliente, "idCliente", "nombre", tb_VentaBoleto.idCliente);
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaBoleto.idEmpleado);
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion", tb_VentaBoleto.idFuncion);
            return View(tb_VentaBoleto);
        }

        // GET: VentaBoletos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_VentaBoleto tb_VentaBoleto = db.tb_VentaBoleto.Find(id);
            if (tb_VentaBoleto == null)
            {
                return HttpNotFound();
            }
            return View(tb_VentaBoleto);
        }

        // POST: VentaBoletos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_VentaBoleto tb_VentaBoleto = db.tb_VentaBoleto.Find(id);
            db.tb_VentaBoleto.Remove(tb_VentaBoleto);
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
    }
}

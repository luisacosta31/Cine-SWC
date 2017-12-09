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
        private tb_VentaBoleto tb = new tb_VentaBoleto();

        // GET: VentaBoletos
        public ActionResult Index()
        {
            var tb_VentaBoleto = db.tb_VentaBoleto.Include(t => t.tb_empleado).Include(t => t.tb_funcion);
            return View(tb_VentaBoleto.ToList());
        }

        public ActionResult VentaBoletosActuales()
        {
            var tb_VentaaBoleto = db.tb_VentaBoleto;
            var lista = tb_VentaaBoleto.ToList().Where(m => ((DateTime)m.fecha).Day.Equals(DateTime.Now.Day));
            return View(lista);
        }

        public ActionResult VentaBoletosxMes(int mes = 0, int mesFinal = 0)
        {
            ViewBag.mes = mes;
            ViewBag.mesFinal = mesFinal;
            var tb_VentaaBoleto = db.tb_VentaBoleto;
            if (mes == 0 || mesFinal == 0)
            {
                return View(tb_VentaaBoleto.ToList());
            }
            else
            {
                if (mes >= mesFinal)
                {
                    ViewBag.mensaje = " el mes inicial no puede ser mayor o igual al mes final";
                }
                   var lista = tb_VentaaBoleto.ToList().Where(m => ((DateTime)m.fecha).Month >= mes && ((DateTime)m.fecha).Month <= mesFinal);
                    return View(lista);
                
                
            }
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
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre");
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion");
            return View();
        }

        // POST: VentaBoletos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVentaB,fecha,idFuncion,idEmpleado,cantidad,total")] tb_VentaBoleto tb_VentaBoleto)
        {
            if (ModelState.IsValid)
            {
                tb_VentaBoleto.idEmpleado = (Int32)Session["idUsuario"];
                tb_VentaBoleto.fecha = DateTime.Today;
                db.tb_VentaBoleto.Add(tb_VentaBoleto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
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
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaBoleto.idEmpleado);
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion", tb_VentaBoleto.idFuncion);
            return View(tb_VentaBoleto);
        }

        // POST: VentaBoletos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVentaB,fecha,idFuncion,idEmpleado,cantidad,total")] tb_VentaBoleto tb_VentaBoleto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_VentaBoleto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
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

        // GET: VentaBoletos/Create
        public ActionResult CreateP()
        {
            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre");
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion");
            return View();
        }

        // POST: VentaBoletos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateP([Bind(Include = "idVentaB,fecha,idFuncion,idEmpleado,cantidad,total")] tb_VentaBoleto tb_VentaBoleto)
        {
            if (ModelState.IsValid)
            {
                tb_VentaBoleto.idEmpleado = (Int32)Session["idUsuario"];
                tb_VentaBoleto.fecha = DateTime.Today;
                db.tb_VentaBoleto.Add(tb_VentaBoleto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEmpleado = new SelectList(db.tb_empleado, "idEmpleado", "nombre", tb_VentaBoleto.idEmpleado);
            ViewBag.idFuncion = new SelectList(db.tb_funcion, "idFuncion", "nombre_funcion", tb_VentaBoleto.idFuncion);
            return View(tb_VentaBoleto);
        }
    }
}

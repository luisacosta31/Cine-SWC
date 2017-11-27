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
    public class tb_funcionController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();

        // GET: tb_funcion
        public ActionResult Index()
        {
            var tb_funcion = db.tb_funcion.Include(t => t.tb_pelicula).Include(t => t.tb_sala);
            return View(tb_funcion.ToList());
        }

        // GET: tb_funcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_funcion tb_funcion = db.tb_funcion.Find(id);
            if (tb_funcion == null)
            {
                return HttpNotFound();
            }
            return View(tb_funcion);
        }

        // GET: tb_funcion/Create
        public ActionResult Create()
        {
            ViewBag.idPelicula = new SelectList(db.tb_pelicula, "idPelicula", "nombre");
            ViewBag.idSala = new SelectList(db.tb_sala, "idSala", "desSala");
            return View();
        }

        // POST: tb_funcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFuncion,nombre_funcion,fecha_inicio,fecha_fin,nroSala,hora_funcion,idPelicula,idSala")] tb_funcion tb_funcion)
        {
            if (ModelState.IsValid)
            {
                db.tb_funcion.Add(tb_funcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPelicula = new SelectList(db.tb_pelicula, "idPelicula", "nombre", tb_funcion.idPelicula);
            ViewBag.idSala = new SelectList(db.tb_sala, "idSala", "desSala", tb_funcion.idSala);
            return View(tb_funcion);
        }

        // GET: tb_funcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_funcion tb_funcion = db.tb_funcion.Find(id);
            if (tb_funcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPelicula = new SelectList(db.tb_pelicula, "idPelicula", "nombre", tb_funcion.idPelicula);
            ViewBag.idSala = new SelectList(db.tb_sala, "idSala", "desSala", tb_funcion.idSala);
            return View(tb_funcion);
        }

        // POST: tb_funcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFuncion,nombre_funcion,fecha_inicio,fecha_fin,nroSala,hora_funcion,idPelicula,idSala")] tb_funcion tb_funcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_funcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPelicula = new SelectList(db.tb_pelicula, "idPelicula", "nombre", tb_funcion.idPelicula);
            ViewBag.idSala = new SelectList(db.tb_sala, "idSala", "desSala", tb_funcion.idSala);
            return View(tb_funcion);
        }

        // GET: tb_funcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_funcion tb_funcion = db.tb_funcion.Find(id);
            if (tb_funcion == null)
            {
                return HttpNotFound();
            }
            return View(tb_funcion);
        }

        // POST: tb_funcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_funcion tb_funcion = db.tb_funcion.Find(id);
            db.tb_funcion.Remove(tb_funcion);
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

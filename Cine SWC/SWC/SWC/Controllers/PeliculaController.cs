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
    public class PeliculaController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();

        // GET: Pelicula
        public ActionResult Index()
        {
            var tb_pelicula = db.tb_pelicula.Include(t => t.tb_censura).Include(t => t.tb_genero);
            return View(tb_pelicula.ToList());
        }

        // GET: Pelicula/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pelicula tb_pelicula = db.tb_pelicula.Find(id);
            if (tb_pelicula == null)
            {
                return HttpNotFound();
            }
            return View(tb_pelicula);
        }

        // GET: Pelicula/Create
        public ActionResult Create()
        {
            ViewBag.idCensura = new SelectList(db.tb_censura, "idCensura", "desCensura");
            ViewBag.idGenero = new SelectList(db.tb_genero, "idGenero", "desGenero");
            return View();
        }

        // POST: Pelicula/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPelicula,nombre,foto,sinopsis,duracion,trailer,pais,idGenero,idCensura")] tb_pelicula tb_pelicula)
        {
            if (ModelState.IsValid)
            {
                db.tb_pelicula.Add(tb_pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCensura = new SelectList(db.tb_censura, "idCensura", "desCensura", tb_pelicula.idCensura);
            ViewBag.idGenero = new SelectList(db.tb_genero, "idGenero", "desGenero", tb_pelicula.idGenero);
            return View(tb_pelicula);
        }

        // GET: Pelicula/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pelicula tb_pelicula = db.tb_pelicula.Find(id);
            if (tb_pelicula == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCensura = new SelectList(db.tb_censura, "idCensura", "desCensura", tb_pelicula.idCensura);
            ViewBag.idGenero = new SelectList(db.tb_genero, "idGenero", "desGenero", tb_pelicula.idGenero);
            return View(tb_pelicula);
        }

        // POST: Pelicula/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPelicula,nombre,foto,sinopsis,duracion,trailer,pais,idGenero,idCensura")] tb_pelicula tb_pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCensura = new SelectList(db.tb_censura, "idCensura", "desCensura", tb_pelicula.idCensura);
            ViewBag.idGenero = new SelectList(db.tb_genero, "idGenero", "desGenero", tb_pelicula.idGenero);
            return View(tb_pelicula);
        }

        // GET: Pelicula/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pelicula tb_pelicula = db.tb_pelicula.Find(id);
            if (tb_pelicula == null)
            {
                return HttpNotFound();
            }
            return View(tb_pelicula);
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_pelicula tb_pelicula = db.tb_pelicula.Find(id);
            db.tb_pelicula.Remove(tb_pelicula);
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

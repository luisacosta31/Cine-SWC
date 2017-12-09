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
    public class EmpleadoController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();        

        // GET: Empleado
        public ActionResult Index()
        {
            var tb_empleado = db.tb_empleado.Include(t => t.tb_Sexo).Include(t => t.tb_tipotrabajador);
            return View(tb_empleado.ToList().Where(e=>e.idEstadoEmpleado.Equals(1)));
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_empleado tb_empleado = db.tb_empleado.Find(id);
            if (tb_empleado == null)
            {
                return HttpNotFound();
            }
            return View(tb_empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo");
            ViewBag.idTipotrab = new SelectList(db.tb_tipotrabajador, "idTipotrab", "desTipotrab");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,nombre,apellidos,dni,sueldo,fecNac,usuario,contra,idTipotrab,idSexo,idEstadoEmpleado")]  tb_empleado tb_empleado)
        {
            if (ModelState.IsValid)
                {
                tb_empleado.idEstadoEmpleado = 1;
                if(tb_empleado.dni.Length!= 8)
                {
                    ViewBag.Message = "El dni debe tener 8 dígitos";
                }
                else
                {
                    var d = db.tb_empleado.Where(a => a.dni.ToString().Equals(tb_empleado.dni.ToString())).FirstOrDefault();

                    if (d != null)
                    {
                        ViewBag.Message = "DNI existente";
                    }
                    else
                    {
                        


                        if (((DateTime)tb_empleado.fecNac).Year>= DateTime.Today.Year -18 )
                        {
                            ViewBag.Message = "Fecha incorrecta";
                        }
                        else
                        {
                            db.tb_empleado.Add(tb_empleado);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        
                    }


                }


            }

            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_empleado.idSexo);
            ViewBag.idTipotrab = new SelectList(db.tb_tipotrabajador, "idTipotrab", "desTipotrab", tb_empleado.idTipotrab);
            return View(tb_empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_empleado tb_empleado = db.tb_empleado.Find(id);
            if (tb_empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_empleado.idSexo);
            ViewBag.idTipotrab = new SelectList(db.tb_tipotrabajador, "idTipotrab", "desTipotrab", tb_empleado.idTipotrab);
            return View(tb_empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,nombre,apellidos,dni,sueldo,fecNac,usuario,contra,idTipotrab,idSexo")] tb_empleado tb_empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_empleado.idSexo);
            ViewBag.idTipotrab = new SelectList(db.tb_tipotrabajador, "idTipotrab", "desTipotrab", tb_empleado.idTipotrab);
            return View(tb_empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_empleado tb_empleado = db.tb_empleado.Find(id);
            if (tb_empleado == null)
            {
                return HttpNotFound();
            }
            return View(tb_empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_empleado tb_empleado = db.tb_empleado.Find(id);
            db.tb_empleado.Where(i => i.idEmpleado.Equals(id)).First().idEstadoEmpleado = 2;
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWC.Models;
using OfficeOpenXml;

namespace SWC.Controllers
{
    public class ClienteController : Controller
    {
        private bd_cine_swcEntities db = new bd_cine_swcEntities();
        private string nombreArchivo;

        // GET: Cliente
        public ActionResult Index()
        {
            var tb_cliente = db.tb_cliente.Include(t => t.tb_Sexo);
            return View(tb_cliente.ToList());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo");
            return View();
        }

        // POST: Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,nombre,apellidos,dni,fecNac,correo,contra,tarjeta,idSexo")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.tb_cliente.Add(tb_cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_cliente.idSexo);
            return View(tb_cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_cliente.idSexo);
            return View(tb_cliente);
        }

        // POST: Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,nombre,apellidos,dni,fecNac,correo,contra,tarjeta,idSexo")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSexo = new SelectList(db.tb_Sexo, "idSexo", "desSexo", tb_cliente.idSexo);
            return View(tb_cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            db.tb_cliente.Remove(tb_cliente);
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
        
        public ActionResult Upload()
        {
            return View("");
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase excelfile)
        {
            if(excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Seleccione un archivo valido";
                return View();
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/import/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        excelfile.SaveAs(path);
                    }
                    else
                    {
                        excelfile.SaveAs(path);
                    }
                    nombreArchivo = excelfile.FileName;
                    int count = 0;
                    ImportarExcel(out count);
                    return View("");
                }
                else
                {
                    ViewBag.Error = "Tipo de archivo incorrecto";
                    return View();
                }
            }
        }

        private bool ImportarExcel(out int count)
        {
            var result = false;
            count = 0;
            try
            {
                String path = Server.MapPath("/") + "\\import\\" + nombreArchivo;
                var package = new ExcelPackage(new System.IO.FileInfo(path));
                int startColumn = 1;
                int startRow = 5;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                object idCliente = null;
                bd_cine_swcEntities db = new bd_cine_swcEntities();
                do
                {
                    idCliente = worksheet.Cells[startRow, startColumn].Value;
                    object nombre = worksheet.Cells[startRow, startColumn + 1].Value.ToString();
                    object apellidos = worksheet.Cells[startRow, startColumn + 2].Value.ToString();
                    object dni = worksheet.Cells[startRow, startColumn + 3].Value.ToString();
                    object fecNac = worksheet.Cells[startRow, startColumn + 4].Value.ToString();
                    object correo = worksheet.Cells[startRow, startColumn + 5].Value.ToString();
                    object contra = worksheet.Cells[startRow, startColumn + 6].Value.ToString();
                    object tarjeta = worksheet.Cells[startRow, startColumn + 7].Value.ToString();
                    object idSexo = worksheet.Cells[startRow, startColumn + 8].Value.ToString();
                    if (idCliente != null && nombre != null&&apellidos!=null && dni != null && fecNac!=null && correo!=null &&contra!=null &&tarjeta !=null &&idSexo!=null)
                    {
                        var isSucces = saveClass(nombre.ToString(), apellidos.ToString(), dni.ToString(), DateTime.Parse(fecNac.ToString()), correo.ToString(), contra.ToString(), tarjeta.ToString(), int.Parse(idSexo.ToString()), db);
                        if (isSucces)
                        {
                            count++;
                        }
                    }
                    startRow++;
                }
                while (idCliente != null);
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        private bool saveClass(string nombre, string apellidos, string dni, DateTime fecNac, string correo, string contra, string tarjeta, int idSexo, bd_cine_swcEntities db)
        {
            var result = false;
            try
            {
                if(db.tb_cliente.Where(t=> t.dni.Equals(dni)).Count() == 0)
                {
                    var item = new tb_cliente();
                    item.nombre = nombre;
                    item.apellidos = apellidos;
                    item.dni = dni;
                    item.fecNac = fecNac;
                    item.correo = correo;
                    item.contra = contra;
                    item.tarjeta = tarjeta;
                    item.idSexo = idSexo;
                    db.tb_cliente.Add(item);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }
    }
}

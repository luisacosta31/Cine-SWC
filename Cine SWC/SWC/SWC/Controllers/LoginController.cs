using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWC.Models;

namespace SWC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }
        bd_cine_swcEntities modelo = new bd_cine_swcEntities();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tb_empleado usuario)
        {
            if (ModelState.IsValid)
            {                
                var v = modelo.tb_empleado.Where(a => a.usuario.Equals(usuario.usuario) && a.contra.Equals(usuario.contra)).FirstOrDefault();
                if (v != null)
                {
                    if (v.idTipotrab == 1)
                    {
                        Session["Nombres"] = v.nombre + " " + v.apellidos.ToString();
                        Session["Tipo"] = v.tb_tipotrabajador.desTipotrab ;
                        return RedirectToAction("/Login/Administrador");
                    }
                    else
                    {
                        Session["Nombres"] = v.nombre + " " + v.apellidos.ToString();
                        Session["Tipo"] = v.tb_tipotrabajador.desTipotrab;
                        return View("/Login/Cajero");
                    }

                }
                else
                {
                    return RedirectToAction("/Login/Login");
                }
            }
            return View(usuario);
        }

        public ActionResult Administrador()
        {
            return View();
        }

        public ActionResult Cajero()
        {
            return View();
        }
    }
}
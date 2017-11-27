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
        bd_cine_swcEntities modelo = new bd_cine_swcEntities();
        public ActionResult Login()
        {
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string usuario, string contraseña)
        {
            Session.Clear();
            if (ModelState.IsValid)
            {
                var v = modelo.tb_empleado.Where(a => a.usuario.ToLower().Equals(usuario.ToLower()) && a.contra.Equals(contraseña)).FirstOrDefault();

                if (v == null || contraseña == null)
                {
                    ViewBag.Message = "Usuario o contraseña incorrectos.";
                    return View();
                }else if (usuario.ToLower() != v.usuario.ToLower() || contraseña != v.contra)
                {
                    ViewBag.Message = "Usuario o contraseña incorrectos.";
                    return View();
                }
                else
                {
                    if (v.usuario.ToLower().Equals(usuario.ToLower()) && v.contra.Equals(contraseña))
                    {
                        Session["idUsuario"] = v.idEmpleado;
                        Session["tipousuario"] = v.idTipotrab;
                        Session["nombre"] = v.nombre + " " + v.apellidos;
                        Session["destipousuario"] = v.tb_tipotrabajador.desTipotrab;
                        if (Session["tipousuario"].Equals(2))
                        {
                            return RedirectToAction("/Cajero");
                        }
                        else
                        {
                            return RedirectToAction("/Administrador");
                        }
                    }                    
                }
                
            }
            else
            {
                ViewBag.Message = "Usuario o contraseña incorrectos.";
                return View();
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
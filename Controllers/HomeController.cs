using Microsoft.AspNetCore.Mvc;
using Sistema.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Sistema.Data;
using Sistema.Functions;

namespace Sistema.Controllers
{
    public class HomeController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AccesoDatos accesoDatos = new AccesoDatos();
        Seguridad seguridad = new Seguridad();

        string nombre_sesion = new Configuracion().nombre_sesion;

        [HttpGet("")]
        public IActionResult Ingreso()
        {
            if (!seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (TempData["mensaje"] != null)
                {
                    ViewBag.mensaje = TempData["mensaje"].ToString();
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Administracion");
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Ingreso(IngresoModel model)
        {
            if (!seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (ModelState.IsValid)
                {
                    if (accesoDatos.Ingreso(model))
                    {
                        UsuarioDatos usuarioDatos = new UsuarioDatos();
                        Seguridad seg = new Seguridad();
                        UsuarioModel usuario = usuarioDatos.ObtenerPorNombre(model.Usuario);
                        HttpContext.Session.SetString(nombre_sesion, seg.GenerarTokenJWT(usuario.Id, usuario.Nombre, usuario.Id_tipo));
                        return RedirectToAction("Index","Administracion");
                    }
                    else
                    {
                        TempData["mensaje"] = functions.generate_alert("Ingreso inválido", "warning");
                        return RedirectToAction("Ingreso", "Home");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index","Administracion");
            }
        }
    }
}